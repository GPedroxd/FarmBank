using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommand:  ICommand
{
    public SendWppMessageCommand(string email, string userPhoneNumber, decimal ammount)
    {
        Email = email;
        UserPhoneNumber = userPhoneNumber;
        Ammount = ammount;
    }

    public string Email { get; private set; }
    public string UserPhoneNumber { get; private set; }
    public decimal Ammount { get; private set; }
}
