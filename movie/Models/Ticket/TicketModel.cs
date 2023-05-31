using movie.Models.Movie;
using movie.Models.Room;

namespace movie.Models.Ticket;

public class TicketModel : BaseModel
{
    public MovieModel Movie { get; set; } = null!;
    public RoomModel Room { get; set; } = null!;
}