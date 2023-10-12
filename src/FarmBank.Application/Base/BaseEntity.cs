namespace FarmBank.Application.Base;

public interface IBaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }

    void SetUpdateAt()
        => UpdatedAt = DateTime.UtcNow;

}
