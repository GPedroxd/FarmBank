using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Integration;
using FarmBank.Integration.Database;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Refit;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((host, services) =>
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

        services.AddScoped(_ => new MongoContext(host.Configuration["MongoDbConnectionString"], "FarmBank"));
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<NewPixCommand>());
        services.AddScoped<IQRCodeService, QRCodeService>();
        services.AddScoped<IWppService, WppService>();
        services.AddRefitClient<IMercadoPagoApi>(new()
        {
            AuthorizationHeaderValueGetter = (msg, ct) => Task.FromResult(host.Configuration["MercadoPagoApiToken"]!),
        }).ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://api.mercadopago.com");
        });

        services.AddRefitClient<IWppApi>()
        .ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(host.Configuration["WppApiUrl"] ?? "localhost:3333");
        });
    })
    .Build();

await host.RunAsync();
