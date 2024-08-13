using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Event;

namespace Template.Infrastructure.Persistence.NHibernate.Extensions;

public static class NHConfigExtensions
{
    public static IServiceCollection AddNHibernate(
        this IServiceCollection services,
        string connectionString
    )
    {
        var sessionFactory = Fluently.Configure()
            .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHUnitOfWork>())
            .ExposeConfiguration(c => {
                c.EventListeners.DeleteEventListeners = new IDeleteEventListener[] { new NHEventListener() };
                c.AppendListeners(ListenerType.PreInsert, new IPreInsertEventListener[] { new NHEventListener() });
                c.AppendListeners(ListenerType.PreUpdate, new IPreUpdateEventListener[] { new NHEventListener() });
            })
            .BuildSessionFactory();

        services.AddSingleton(sessionFactory);

        return services;
    }
}
