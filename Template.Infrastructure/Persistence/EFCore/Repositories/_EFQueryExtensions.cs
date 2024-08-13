using Template.Domain.Seedwork;

namespace Template.Infrastructure.Persistence.EFCore.Repositories;

public static class EFQueryExtensions
{
    public static IQueryable<T> CheckPagination<T>(this IQueryable<T> query, QueryFilter filter) where T : Entity
    {
        if (!filter.Paginated) return query;

        return query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }
    public static IEnumerable<T> CheckPagination<T>(this IEnumerable<T> list, QueryFilter filter) where T : Entity
    {
        if (!filter.Paginated) return list;

        return list
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }
}
