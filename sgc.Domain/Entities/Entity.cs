namespace sgc.Domain.Entities;

public abstract class Entity
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    protected void Update() => UpdatedAt = DateTime.UtcNow;
}
