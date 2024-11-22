using FarmBank.Application.Communication;
using FarmBank.Application.Communication.Handlers.NewDepositMade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBank.Application.WppCommands.Commands.Rank;

public class RankReplyMessage : ICommunicationMessage

{
    

    public string EventName { get; init; }
    public IEnumerable<Deposit> Placements { get; init; }

    public string GetFormatedMessage()
    {
        var standingsString = string.Empty;

        var pos = 1;

        foreach (var standing in Placements.OrderByDescending(o => o.TotalDeposited))
        {
            var emoji = "🪙";
            if (pos == 1)
                emoji = "💎";
            else if (pos == 2)
                emoji = "💰";
            else if (pos == 3)
                emoji = "🤑";

            standingsString += $"{emoji} {pos++}° {standing.MemberName} - R${standing.TotalDeposited.ToString("#.##")}\r\n";
        }

        var massageFullfield = TEMPLATE.
                Replace("@EVENT", EventName).
                Replace("@TOTAL",Placements.Sum(s=> s.TotalDeposited).ToString("#.##")).
                Replace("@STANDINGS", standingsString);

        return massageFullfield;
    }

    private static string TEMPLATE = 
                                    "*@EVENT*\r\n\r\n" +
                                    "Total depositado: R$@TOTAL\r\n" +
                                    "@STANDINGS\r\n" +
                                    "@LINK";

    public RankReplyMessage(string eventName, IEnumerable<Deposit> placements)
    {
        EventName = eventName;
        Placements = placements;
    }
}

public record Deposit
{
    public string MemberName { get; init; }
    public decimal TotalDeposited { get; init; }
}
