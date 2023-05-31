namespace movie.Models.Movie;

public class MovieEdit
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int Price { get; set; }
}