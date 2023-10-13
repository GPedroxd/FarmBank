using FarmBank.Application.Base;

namespace FarmBank.Application.Models;

public class Member : IBaseEntity
{
    public Member()
    { }

    public Member(string name, string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Name = name;
    }
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get ; set ; }
    public string Name{ get; init; }
    public string PhoneNumber { get; private set; }
    
    public List<Deposit> Deposits { get ; set ; } = new();
    
    public void AddDeposit(Deposit deposit)
    {
        if(Deposits.Any(a => a.TransactionId.Equals(deposit.TransactionId)))
            return;

        Deposits.Add(deposit);
    }
    public decimal TotalDeposited { get => Deposits.Sum(s => s.Amount); set =>  _ = value; }
}

public class Deposit
{
    public Deposit()
    { }

    public Deposit(string transactionId , decimal amount)
    {
        Amount = amount;
        TransactionId = transactionId;
    }

    public string TransactionId { get; init; }
    public DateTime DepositDate { get; init; } = DateTime.UtcNow;
    public decimal Amount { get; init; }
}