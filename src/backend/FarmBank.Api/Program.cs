using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FarmBank.Integration;
using FarmBank.Integration.Interfaces;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<NewPixCommand>());
builder.Services.AddTransient<IQRCodeService, QRCodeService>();
builder.Services.AddRefitClient<IMercadoPagoApi>(new() {
    AuthorizationHeaderValueGetter = (msg, ct) => Task.FromResult(builder.Configuration["MercadoPagoApiToken"]!),
}).ConfigureHttpClient(c => {
    c.BaseAddress = new Uri("https://api.mercadopago.com");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
