using FarmBank.Application.Communication;
using FarmBank.Core.Member;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.Join;

public class JoinWppCommandHandler : IWppCommand
{
    private readonly ILogger<JoinWppCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;
    private readonly ICommunicationService _communicationService;

    public JoinWppCommandHandler(ILogger<JoinWppCommandHandler> logger, IMemberRepository memberRepository, ICommunicationService communicationService)
    {
        _logger = logger;
        _memberRepository = memberRepository;
        _communicationService = communicationService;
    }

    public async Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        var senderName = inputMessage.SenderName;
        var senderPhone = inputMessage.SenderId.Split(":")[0];

        var phoneStandar = string.Join("", senderPhone.Skip(2)) ?? throw new ArgumentException("not able to get phone number");

        var currentMember = await _memberRepository.GetByPhoneNumberAsync(phoneStandar, CancellationToken.None);

        if (currentMember is not null)
        {
            await _communicationService.SendMessagemAsync(new JoinReplyMessage(false),inputMessage.Message.Id);
            return;
        }

        var member = new Core.Member.Member(senderName, phoneStandar);

        await _memberRepository.InsertAsync(member, CancellationToken.None);

        await _communicationService.SendMessagemAsync(new JoinReplyMessage(true), inputMessage.Message.Id);
    }
}
