namespace FarmBank.Application.Interfaces;

public interface IWppService
{
    Task SendMessagemAsync(string message);
}
