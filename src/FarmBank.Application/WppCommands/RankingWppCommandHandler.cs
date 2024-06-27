
using FarmBank.Application.Interfaces;
using FarmBank.Application.Member;

namespace FarmBank.Application.WppCommands;

public class RankingWppCommandHandler : IWppCommandHandler
{
    private readonly IMemberRepository _memberRepository;
    private readonly IWppService _wppService;

    public RankingWppCommandHandler(IMemberRepository memberRepository, IWppService wppService)
    {
        _memberRepository = memberRepository;
        _wppService = wppService;
    }

    public async Task Handler(WppSenderInfo senderInfo, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken);

        var rankingWppMessage = new RankingWppMessage(members);

       await _wppService.SendMessagemAsync(rankingWppMessage, cancellationToken);
    }
}
