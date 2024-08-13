namespace Template.Domain.Seedwork;

public abstract class QueryFilter
{
    public bool Paginated { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;    
    public string? Search { get; set; } = null;
    
    protected QueryFilter(QueryFilter other)
    {
        Paginated = other.Paginated;
        Page = other.Page;
        PageSize = other.PageSize;
        Search = other.Search;
    }

    protected QueryFilter()
    {
    }
}
