namespace FarmBank.Application.Base;

public class BaseEntity
{
    public BaseEntity(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    public BaseEntity()
    {
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public void SetUpdateAt()
        => UpdatedAt = DateTime.UtcNow;

}
