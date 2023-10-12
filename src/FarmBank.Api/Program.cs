using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Integration;
using FarmBank.Integration.Database;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.Repository;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
builder.Services.AddScoped(_ => new MongoContext(builder.Configuration["MongoDbConnectionString"], "FarmBank"));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<NewPixCommand>());
builder.Services.AddScoped<IQRCodeService, QRCodeService>();
builder.Services.AddScoped<IWppService, WppService>();

builder.Services.AddRefitClient<IMercadoPagoApi>(new()
{
    AuthorizationHeaderValueGetter = (msg, ct) => Task.FromResult(builder.Configuration["MercadoPagoApiToken"]!),
}).ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://api.mercadopago.com");
});

builder.Services.AddRefitClient<IWppApi>()
.ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["WppApiUrl"] ?? "localhost:3333");
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
