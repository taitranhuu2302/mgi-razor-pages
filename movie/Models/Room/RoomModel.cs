using movie.Models.Movie;

namespace movie.Models.Room;

public class RoomModel : BaseModel
{
    public string Name { get; set; } = null!;
    public MovieModel? Movie { get; set; } = null!;

    public RoomModel()
    {
    }
}