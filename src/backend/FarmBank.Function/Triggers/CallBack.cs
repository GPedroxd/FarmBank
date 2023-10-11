using System.Net;
using System.Text.Json;
using FarmBank.Application.Commands.UpdateTransaction;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FarmBank.Function.Triggers
{
    public class CallBack
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public CallBack(ILoggerFactory loggerFactory, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<NewPix>();
            _mediator = mediator;
        }

        [Function("callback")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            UpdateTransactionCommand? pixCommand;
            try
            {
                pixCommand = JsonSerializer.Deserialize<UpdateTransactionCommand>(req.Body);

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

            await _mediator.Send(pixCommand);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
    }
}
