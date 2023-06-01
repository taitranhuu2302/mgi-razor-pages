using System.ComponentModel.DataAnnotations;

namespace movie.Models.Room;

public class RoomCreate
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Seats { get; set; }
}