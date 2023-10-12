using System.Net;
using System.Text.Json;
using FarmBank.Application.Commands.NewPix;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FarmBank.Function.Triggers
{
    public class NewPix
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public NewPix(ILoggerFactory loggerFactory, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<NewPix>();
            _mediator = mediator;
        }

        [Function("newPix")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            NewPixCommand? pixCommand;
            try
            {
                var json = await req.ReadAsStringAsync();
                _logger.LogInformation(json);
                pixCommand = JsonSerializer.Deserialize<NewPixCommand>(json ?? "", new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true });

                if (pixCommand is null)
                    throw new Exception("Unable to serialize the request body");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var responseFail = req.CreateResponse(HttpStatusCode.BadRequest);
                responseFail.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                responseFail.WriteString("Unable to serialize the request body");

                return responseFail;
            }

            var result = await _mediator.Send(pixCommand);

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.WriteString(JsonSerializer.Serialize(result));

            return response;
        }
    }
}
