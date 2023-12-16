using FarmBank.Application.Base;

namespace FarmBank.Application.BackgroundServices.CountdownBackgroundService;

public class CountdownWppMessage : IBaseWppMessage
{
    public static DateTime FARMDAY = new DateTime(2024, 01, 19);
    private int daysLeft;
    private readonly string _link;
    public CountdownWppMessage(DateTime dateRefer, string link)
    {
        daysLeft = (FARMDAY - dateRefer).Days;
        _link = link;
    }

    public string GetFormatedMessage()
    {
        if(daysLeft > 0)
            return MoreThenZeroDaysLeftMessage.Replace("@DAYSLEFT", daysLeft.ToString())
                .Replace("@LINK", _link ?? "https://localhost:5000");
        
        return ZeroDaysLeftMessage;
    }

    private string MoreThenZeroDaysLeftMessage = "🤑💸🐄🏦3️⃣▪️0️⃣💸🤑\r\n \r\n " +
                                                "Meus caros humanos, tenho o prazer de anunciar que faltam\r\n" +
                                                "*@DAYSLEFT DIAS*\r\n"+
                                                "para a fazendinha(aka mineirinho).\r\n \r\n"+
                                                "Contribua para a fezendinha clicando link abaixo: \r\n \r\n" +
                                                "@LINK \r\n \r\n"+
                                                "( *OBS*: O total em conta pode ser diferente do pago devido a taxa de 1% do mercado pago.)";

    private string ZeroDaysLeftMessage = "🤑💸🐄🏦3️⃣▪️0️⃣💸🤑\r\n \r\n" +
                                        "🎊 *QUEM TA TRISTE NÃO TA MAIS* 🎊\r\n \r\n" +
                                        "Chegou o grande dia da fazendinha(aka mineirinho)!!!\r\n \r\n"+
                                        "( *OBS*: Não esqueça de organizar as suas coisas, como toalhas, roupas e protetor solar e fique esperto ao horario, não gostamos de atrasadinhos.)";
}
