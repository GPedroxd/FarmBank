using FarmBank.Application;
using FarmBank.Application.Communication;
using FarmBank.Application.Payment;
using FarmBank.Integration.Communication;
using FarmBank.Integration.DataAccess.Database;
using FarmBank.Integration.PaymentGateway;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Net.Http;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDbRepositories();

builder.Services.AddApplicationDependencies();
builder.Services.AddTransient<IPaymentGatewayService, MercadoPagoPaymentGateway>();
builder.Services.AddTransient<ICommunicationService, WppService>();

var wppConfig = new WppConfigs(
    builder.Configuration["WppGroupId"],
    builder.Configuration["WppBotInstanceKey"],
    builder.Configuration["FrontEndUrl"],
    builder.Configuration["WppUsername"],
    builder.Configuration["WppPassword"]
);

builder.Services.AddSingleton(wppConfig);

var cred = Convert.
            ToBase64String(
            Encoding.UTF8
                    .GetBytes($"{wppConfig.UserName}:{wppConfig.Password}"));

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
    .Services.AddRefitClient<IWppApi>(new()
    {
        AuthorizationHeaderValueGetter = (msg, ct_) => 
        Task.FromResult(cred)
    })
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
    c.WithMethods("*");
    c.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
