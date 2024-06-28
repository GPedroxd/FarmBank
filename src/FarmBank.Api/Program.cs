using FarmBank.Api.BackgroundService;
using FarmBank.Application.Base;
using FarmBank.Application.Communication;
using FarmBank.Application.Dto;
using FarmBank.Application.Payment;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Integration.Communication;
using FarmBank.Integration.DataAccess.Database;
using FarmBank.Integration.PaymentGateway;
using Polly;
using Polly.Extensions.Http;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDbRepositories();

builder.Services.AddBackgroundService();

builder.Services.AddMediatR(
    conf => conf.RegisterServicesFromAssemblyContaining<NewPaymentCommand>()
);
builder.Services.AddScoped<EventDispatcher>();
builder.Services.AddScoped<IPaymentGatewayService, MercadoPagoPaymentGateway>();
builder.Services.AddTransient<ICommunicatonService, WppService>();

var wppConfig = new GeneralConfigs(
    builder.Configuration["WppGroupId"],
    builder.Configuration["WppBotInstanceKey"],
    builder.Configuration["FrontEndUrl"]
);
builder.Services.AddSingleton(wppConfig);

builder
    .Services.AddRefitClient<IMercadoPagoApi>(
        new()
        {
            AuthorizationHeaderValueGetter = (msg, ct) =>
                Task.FromResult(builder.Configuration["MercadoPagoApiToken"]!),
        }
    )
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://api.mercadopago.com");
        c.Timeout = TimeSpan.FromMinutes(3);
    });

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => !msg.IsSuccessStatusCode)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

builder
    .Services.AddRefitClient<IWppApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["WppApiUrl"] ?? "localhost:3333");
        c.Timeout = TimeSpan.FromMinutes(3);
    })
    .AddPolicyHandler(retryPolicy);

builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FarmBank.Api v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.WithMethods("POST");
    c.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
