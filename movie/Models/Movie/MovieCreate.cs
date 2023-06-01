using System.ComponentModel.DataAnnotations;

namespace movie.Models.Movie;

public class MovieCreate
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Image { get; set; } = null!;
    [Required]
    public int Price { get; set; }
    [Required]
    public string RoomId { get; set; } = null!;
    [Required]
    public int TicketCount { get; set; }
}