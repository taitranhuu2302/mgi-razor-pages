namespace movie.Models;

public class PaginateModel
{
    public int PageSize { get; set; } = 5;
    public int Count { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPage => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
}