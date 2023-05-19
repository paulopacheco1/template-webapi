
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Application.SeedWork;

public class Paginated<T>
{
    public IEnumerable<T> Data { get; private set; }
    public int Count { get; private set; }
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int LastPage { get; private set; }

    public Paginated(IEnumerable<T> data, int count, QueryFilter filter)
    {
        Data = data;
        Count = count;
        PageSize = filter.PageSize;
        CurrentPage = filter.Page;
        LastPage = (count + filter.PageSize - 1) / filter.PageSize;
    }
}
