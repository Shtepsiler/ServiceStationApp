using System.Text.Json.Serialization;

public class Pagination<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public List<T> Items { get; set; } = new();

    public Pagination() { }

    [JsonConstructor]
    public Pagination(List<T> items, int totalPages, int currentPage, int pageSize)
    {
        Items = items;
        TotalPages = totalPages;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }

    public static Pagination<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var result = new Pagination<T>
        {
            Items = items,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            HasPrevious = pageNumber > 1
        };
        result.HasNext = result.CurrentPage < result.TotalPages;
        return result;
    }
}