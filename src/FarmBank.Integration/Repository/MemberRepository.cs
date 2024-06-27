using FarmBank.Application.Member;
using FarmBank.Integration.Database;

namespace FarmBank.Integration.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly MongoContext _context;

    public MemberRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Member.FilterByAsync(f =>  true);

    public async Task<Member> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        => await _context.Member.FirstOrDefaultAsync(f => f.PhoneNumber.Equals(phoneNumber));

    public async Task InsertAsync(Member member, CancellationToken cancellationToken)
         => await _context.Member.InsertAsync(member);

    public async Task ReplaceAsync(Member member, CancellationToken cancellationToken)
        => await _context.Member.ReplaceAsync(member);
}
