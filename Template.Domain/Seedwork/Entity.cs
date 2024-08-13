namespace Template.Domain.Seedwork;

public abstract class Entity
{
    protected Entity() { }

    public virtual Guid Id { get; private set; }
    public virtual DateTime CreatedAt { get; private set; }
    public virtual DateTime UpdatedAt { get; private set; }
    public virtual DateTime? DeletedAt { get; private set; }

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
