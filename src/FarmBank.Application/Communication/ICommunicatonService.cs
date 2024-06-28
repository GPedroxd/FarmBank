using FarmBank.Application.Base;

namespace FarmBank.Application.Communication;

public interface ICommunicatonService
{
    Task SendMessagemAsync(IBaseWppMessage message, CancellationToken cancellationToken);
}