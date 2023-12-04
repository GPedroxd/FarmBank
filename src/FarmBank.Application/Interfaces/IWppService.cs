using FarmBank.Application.Base;

namespace FarmBank.Application.Interfaces;

public interface IWppService
{
    Task SendMessagemAsync(IBaseWppMessage message, CancellationToken cancellationToken);
}