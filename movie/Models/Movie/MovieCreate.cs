namespace movie.Models.Movie;

public class MovieCreate
{
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int Price { get; set; }
    public string RoomId { get; set; } = null!;
    public int TicketCount { get; set; }
}