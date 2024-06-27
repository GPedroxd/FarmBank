using FarmBank.Application.Models;

namespace FarmBank.Application.Member;

public interface IMemberRepository
{
    Task InsertAsync(Member member, CancellationToken cancellationToken);
    Task ReplaceAsync(Member member, CancellationToken cancellationToken);
    Task<Member> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<IEnumerable<Member>> GetAllAsync(CancellationToken cancellationToken);
}
