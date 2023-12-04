using System.Text;
using FarmBank.Application.Base;
using FarmBank.Application.Models;

namespace FarmBank.Application.WppCommands;
public class RankingWppMessage : IBaseWppMessage
{
    private IEnumerable<Member> _members;

    public RankingWppMessage(IEnumerable<Member> members)
    {
        _members = members;
    }

    public string GetFormatedMessage()
    {
        var memberOrdeds = _members.OrderByDescending(o=> o.TotalDeposited);

        var position = 1;

        var sb = new StringBuilder();
        sb.Append(top);
        
        foreach (var member in memberOrdeds)
        {
            if(position == 1)
            {
                sb.AppendLine($"💎 1° - {member.Name}, com {member.TotalDeposited} lules!");
                position++;
                continue;
            }

            if(position == 2)
            {
                sb.AppendLine($"💰 2° - {member.Name}, com {member.TotalDeposited} lules!");
                position++;
                continue;
            }

            if(position == 3)
            {
                sb.AppendLine($"🤑 3° - {member.Name}, com {member.TotalDeposited} lules!");
                position++;
                continue;
            }

            sb.AppendLine($"🪙 {position}° - {member.Name}, com {member.TotalDeposited} lules!");

            position++;
        }

        sb.Append(footer);

        return sb.ToString();
    }

    private static string top = "🤑💸🐄🏦3️⃣▪0️⃣🐄💸🤑 \r\n \r\n"+
                                    " 💲💲 *Top Donates* 💲💲\r\n \r\n";                             
    private static string footer = "\r\nContribua você também clicando no link abaixo: "+
                                    "\r\n \r\nhttps://farmbank-front.vercel.app/" +
                                    "\r\n \r\n( OBS: O total em conta pode ser diferente do pago devido a taxa de 1% do mercado pago.)";
                                

}
