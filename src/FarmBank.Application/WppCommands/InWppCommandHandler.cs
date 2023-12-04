
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;

namespace FarmBank.Application.WppCommands;

public class InWppCommandHandler : IWppCommandHandler
{
    private readonly IMemberRepository _memberRepository;
    private readonly IWppService _wppService;

    public InWppCommandHandler(IMemberRepository memberRepository, IWppService wppService)
    {
        _memberRepository = memberRepository;
        _wppService = wppService;
    }

    public async Task Handler(WppSenderInfo senderInfo, CancellationToken cancellationToken)
    {
        var memberName = senderInfo.MemberName;
        var phoneNumber =  senderInfo.PhoneNumber.Split('@')[0].Remove(0, 2);

        var member = await _memberRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if(member is not null)
            return;

        var newMember = new Member(memberName, phoneNumber);

        await _memberRepository.InsertAsync(member, cancellationToken);

        //send welcome message here
    }
}
