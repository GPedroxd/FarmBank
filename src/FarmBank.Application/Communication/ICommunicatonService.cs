namespace FarmBank.Application.Communication;

public interface ICommunicatonService
{
    Task SendMessagemAsync(CommunicationMessage message, CancellationToken cancellationToken);
}