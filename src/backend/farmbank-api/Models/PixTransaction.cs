namespace farmbank_api.Models;

public class PixTransaction
{
    public string TransactionId { get; init;}
    public string UserName { get; init; }
    public string UserPhone { get; init; }
    public decimal Value { get; init; }
    public TransactionState State { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public PixTransaction(string transactionId, string userName, string userPhone, decimal value)
    {
        TransactionId = transactionId;
        UserName = userName;
        UserPhone = userPhone;
        Value = value;
        State = TransactionState.Pending;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public PixTransaction(string transactionId, string userName, string userPhone, decimal value, TransactionState state, DateTime createdAt, DateTime updatedAt)
    {
        TransactionId = transactionId;
        UserName = userName;
        UserPhone = userPhone;
        Value = value;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public void Confirm()
    {
        State = TransactionState.Confirmed;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Cancel()
    {
        State = TransactionState.Canceled;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum TransactionState 
{
    Pending,
    Confirmed,
    Canceled
}