namespace FarmBank.Application.WppCommands;

public interface IWppCommandHandler
{
    Task Handler(WppSenderInfo senderInfo, CancellationToken cancellationToken);
}
