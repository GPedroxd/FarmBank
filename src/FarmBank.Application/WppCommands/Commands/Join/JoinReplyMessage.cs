using FarmBank.Application.Communication;

namespace FarmBank.Application.WppCommands.Commands.Join;

public class JoinReplyMessage : ICommunicationMessage
{
    private readonly string _message;

    public JoinReplyMessage(bool isJoinMessage)
    {
        _message = isJoinMessage ? JOINEDMESSAGE : ALREADYJOINED;  
    }

    public string GetFormatedMessage()
        => _message;
    

    private const string JOINEDMESSAGE = "Feito! \r\n" +
                                        "Agora faça o pix de imediato.";

    private const string ALREADYJOINED = "vc não pode juntar o que já ta juntado. \r\n" +
                                        "Agora faça o pix de imediato.";
}
