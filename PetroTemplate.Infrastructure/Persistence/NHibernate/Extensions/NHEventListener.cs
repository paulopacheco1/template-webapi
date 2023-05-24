using System.Reflection;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Infrastructure.Persistence.NHibernate.Extensions;

public class NHEventListener : DefaultDeleteEventListener, IPreInsertEventListener, IPreUpdateEventListener
{
    public NHEventListener()
    {
    }

    protected override void DeleteEntity(IEventSource session, object entity,
        EntityEntry entityEntry, bool isCascadeDeleteEnabled,
        IEntityPersister persister, ISet<object> transientEntities)
    {
        if (entity is Entity)
        {
            var e = (Entity)entity;
            e.GetType().GetMethod("SetDeleted", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entity, null);

            CascadeBeforeDelete(session, persister, entity, entityEntry, transientEntities);
            CascadeAfterDelete(session, persister, entity, transientEntities);
        }
        else
        {
            base.DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled,
                              persister, transientEntities);
        }
    }

    public bool OnPreInsert(PreInsertEvent @event)
    {
        var entity = @event.Entity as Entity;
        if (entity == null)
            return false;

        SetCreated(entity, @event.Persister, @event.State);
        return false;
    }

    public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
    {
        var entity = @event.Entity as Entity;
        if (entity == null)
            return Task.FromResult(false);

        SetCreated(entity, @event.Persister, @event.State);
        return Task.FromResult(false);
    }

    public bool OnPreUpdate(PreUpdateEvent @event)
    {
        var entity = @event.Entity as Entity;
        if (entity == null)
            return false;

        SetUpdated(entity, @event.Persister, @event.State);
        return false;
    }

    public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
    {
        var entity = @event.Entity as Entity;
        if (entity == null)
            return Task.FromResult(false);

        SetUpdated(entity, @event.Persister, @event.State);
        return Task.FromResult(false);
    }

    private void SetCreated(Entity entity, IEntityPersister persister, object[] state)
    {
        entity.GetType().GetMethod("SetCreated", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entity, null);

        SetState(persister, state, nameof(entity.CreatedAt), entity.CreatedAt);
        SetState(persister, state, nameof(entity.UpdatedAt), entity.UpdatedAt);
    }

    private void SetUpdated(Entity entity, IEntityPersister persister, object[] state)
    {
        entity.GetType().GetMethod("SetUpdated", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entity, null);

        SetState(persister, state, nameof(entity.UpdatedAt), entity.UpdatedAt);
    }

    private void SetState(IEntityPersister persister, object[] state, string propertyName, object value)
    {
        var index = Array.IndexOf(persister.PropertyNames, propertyName);
        if (index == -1)
            return;
        state[index] = value;
    }
}