using FarmBank.Application.Communication;

namespace FarmBank.Application.WppCommands;

public class ErrorDefaultMessage : ICommunicationMessage
{
    public string GetFormatedMessage()
    {
        return "Ocorreu um erro processando seu comando.";
    }
}
