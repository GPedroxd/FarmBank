using FarmBank.Application.Base;
using FarmBank.Core.Member;

namespace FarmBank.Application.Member.Commands.NewMember;

public class NewMemberCommandHandler : ICommandHandler<NewMemberCommand, ResponseResult>
{
    private readonly IMemberRepository _memberRepository;
    private readonly EventDispatcher _dispatcher;

    public NewMemberCommandHandler(IMemberRepository memberRepository, EventDispatcher dispatcher)
    {
        _memberRepository = memberRepository;
        _dispatcher = dispatcher;
    }

    public async Task<ResponseResult> Handle(NewMemberCommand request, CancellationToken cancellationToken)
    {
        var validator = new NewMemberCommandValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return new(validationResult.Errors);

        var member = new Core.Member.Member(request.Name, request.PhoneNumber);

        await _memberRepository.InsertAsync(member, cancellationToken);

        await _dispatcher.DispatchEvents(member, cancellationToken);

        return  ResponseResult.ValidResult();
    }
}
