namespace movie.Models;

public class PaginateResponse<T>
{
    public int PageSize { get; set; } = 5;
    public int Count { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPage => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public List<T> Data { get; set; } = new List<T>();
}