namespace PetroTemplate.Domain.Seedwork;

public abstract class Entity
{
    protected Entity() { }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    protected virtual void SetCreated()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
    }

    protected virtual void SetUpdated()
    {
        UpdatedAt = DateTime.Now;
    }

    protected virtual void SetDeleted()
    {
        DeletedAt = DateTime.Now;
    }

    protected virtual void RestoreDeleted()
    {
        DeletedAt = null;
        UpdatedAt = DateTime.Now;
    }
}
