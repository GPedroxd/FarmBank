using FarmBank.Application.Base;

namespace FarmBank.Application.Member.Commands.NewMemberDeposit;

public struct NewDepositWppMessage : IBaseWppMessage
{
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public decimal MemberTotalAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Link { get; set; }

    public NewDepositWppMessage(string userName, decimal amount, decimal memberTotalAmount, decimal totalAmount, string link)
    {
        UserName = userName;
        Amount = amount;
        MemberTotalAmount = memberTotalAmount;
        TotalAmount = totalAmount;
        Link = link;
    }
    public string GetFormatedMessage()
     => Deposit.Replace("@NAME", UserName).
                            Replace("@AMMOUNT", Amount.ToString()).
                            Replace("@MEMBERAMOUNTTOTAL", MemberTotalAmount.ToString()).
                            Replace("@TOTALAMOUNT", TotalAmount.ToString()).
                            Replace("@LINK", Link ?? "https://localhost:5000");


    public static string Deposit = "🤑💸🐄🏦3️⃣▪️0️⃣💸🤑\r\n \r\n *O nosso(a) querido(a) @NAME contribuiu com @AMMOUNT lules para a fazendinha.* \r\n " +
                                    "👏🏼👏🏼👏🏼👏🏼👏🏼👏🏼👏🏼 \r\n \r\n" +
                                    "@NAME já contribuiu com o total de @MEMBERAMOUNTTOTAL lules.\r\n \r\n" +
                                    "*Agora temos um total de @TOTALAMOUNT lules.*\r\n💸💸💸💸💸💸💸 \r\n \r\n" +
                                    "Contribua você também clicando no link abaixo: \r\n \r\n" +
                                    "@LINK \r\n \r\n" +
                                    "( *OBS*: O total em conta pode ser diferente do pago devido a taxa de 1% do mercado pago.)";


}
