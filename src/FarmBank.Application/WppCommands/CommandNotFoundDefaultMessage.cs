using FarmBank.Application.Communication;

namespace FarmBank.Application.WppCommands;

public class CommandNotFoundDefaultMessage : ICommunicationMessage
{
    public string GetFormatedMessage()
    {
        return "Comando não encontrado ou não permitido.";
    }
}
