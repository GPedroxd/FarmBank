namespace FarmBank.Application.Communication;

public interface ICommunicationService
{
    Task SendMessagemAsync(ICommunicationMessage message, string? replyTo = null, CancellationToken cancellationToken = default);
}