using FarmBank.Application.Base;
using FarmBank.Application.Commands.SendWppMessage;
using FarmBank.Application.Interfaces;
using MediatR;

namespace FarmBank.Application.Commands.NewMemberDeposit;

public class NewMemberDepositCommandHandler : ICommandHandler<NewMemberDepositCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMediator _mediator;

    public NewMemberDepositCommandHandler(IMemberRepository memberRepository, IMediator mediator, ITransactionRepository transactionRepository)
    {
        _memberRepository = memberRepository;
        _mediator = mediator;
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(NewMemberDepositCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

        if(member is null) return;

        member.AddDeposit (new Models.Deposit(request.TransactionId, request.Amount));

        await _memberRepository.ReplaceAsync(member, cancellationToken);

        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        await _mediator.Send(new SendWppMessageCommand(member.Name, request.Amount, member.TotalDeposited, totalAmmount));
    }
}
