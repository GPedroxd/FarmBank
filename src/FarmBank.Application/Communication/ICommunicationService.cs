namespace FarmBank.Application.Communication;

public interface ICommunicationService
{
    Task SendMessagemAsync(ICommunicationMessage message, CancellationToken cancellationToken);
}