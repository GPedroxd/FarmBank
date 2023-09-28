using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommand:  ICommand
{
    public string Email { get; set; }
    public string UserPhoneNumber { get; set; }
    public decimal Ammount { get; set; }
}
