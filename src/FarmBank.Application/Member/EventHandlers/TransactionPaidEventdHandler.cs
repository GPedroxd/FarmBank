using FarmBank.Application.Communication;
using FarmBank.Application.Dto;
using FarmBank.Core.Event;
using FarmBank.Core.Transaction;
using FarmBank.Core.Transaction.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Member.EventHandlers;

public class TransactionPaidEventdHandler : INotificationHandler<TransactionPaidEvent>
{
    private readonly ILogger<TransactionPaidEventdHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICommunicatonService _wppService;
    private readonly GeneralConfigs _configs;


    public TransactionPaidEventdHandler(ITransactionRepository transactionRepository,
        ICommunicatonService wppService, GeneralConfigs configs, ILogger<TransactionPaidEventdHandler> logger, IEventRepository eventRepository)
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
        _wppService = wppService;
        _configs = configs;
    }

    public Task Handle(TransactionPaidEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
