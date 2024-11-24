using FarmBank.Application.Communication;

namespace FarmBank.Application.WppCommands.Commands.Pix;

public class PixReplyMessage : ICommunicationMessage
{
    private readonly decimal _amount;
    private readonly string _token;
    private readonly bool _error;


    public PixReplyMessage(decimal amount, string message, bool error = false)
    {
        _amount = amount;
        _token = message;
        _error = error;
    }

    public string GetFormatedMessage()
    {
        if (_error)
            return _token;

        var farmatedMessage = TEMPLATE.Replace("@VALOR", _amount.ToString("#.##"))
            .Replace("@TOKEN", _token);

        return farmatedMessage;
    }

    private const string TEMPLATE = "Aqui está o copia e cola no valor de R$@VALOR. \r\n" +
                                    "@TOKEN";   
}
