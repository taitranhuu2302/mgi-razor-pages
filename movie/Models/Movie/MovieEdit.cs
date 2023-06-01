using System.ComponentModel.DataAnnotations;

namespace movie.Models.Movie;

public class MovieEdit
{
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Image { get; set; } = null!;
    [Required]
    public int Price { get; set; }
}