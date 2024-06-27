using FarmBank.Application.Base;
using FarmBank.Application.Models;

namespace FarmBank.Application.Member.Commands.NewMember;

public class NewMemberCommandHandler : ICommandHandler<NewMemberCommand, ResponseResult<Member>>
{
    private readonly IMemberRepository _memberRepository;

    public NewMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<ResponseResult<Member>> Handle(NewMemberCommand request, CancellationToken cancellationToken)
    {
        var validator = new NewMemberCommandValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return new(validationResult.Errors);

        var member = new Member(request.Name, request.PhoneNumber);

        await _memberRepository.InsertAsync(member, cancellationToken);

        return new ResponseResult<Member>(member);
    }
}
