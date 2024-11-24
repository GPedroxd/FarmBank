using FarmBank.Application.Communication;
using FarmBank.Core.Event;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.Rank;

public class RankWppCommandHandler : IWppCommand

{
    private readonly IEventRepository _eventRepository;
    private readonly ICommunicationService _communicationService;

    public RankWppCommandHandler(IEventRepository eventRepository, ICommunicationService communicationService)
    {
        _eventRepository = eventRepository;
        _communicationService = communicationService;
    }

    public async Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {

        var eventos = await _eventRepository.GetActivetedEventAsync(CancellationToken.None);
        var eventoAtivo = eventos.First();

        var grouppedDepositByMember = eventoAtivo.Deposits.GroupBy(gb => gb.MemberId);

        var depositsSummarized = grouppedDepositByMember.Select(s =>
        {
            var sub = s.First();

            return new Deposit
            {
                MemberName = sub.MemberName,
                TotalDeposited = s.Sum(s => s.Amount)
            };

        });

        var depositOrderd = depositsSummarized.OrderByDescending(s => s.TotalDeposited).ToList();

        var replyMessage = new RankReplyMessage( eventoAtivo.Name,depositOrderd );

        var teste = replyMessage.GetFormatedMessage();
    }
}
