using movie.Models.Movie;
using movie.Models.User;

namespace movie.Models.Room;

public enum RoomStatus
{
    Available,
    Busy
}

public class RoomModel : BaseModel
{
    public string Name { get; set; } = null!;
    public int Seats { get; set; }
    public RoomStatus RoomStatus { get; set; } = RoomStatus.Available;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public UserModel? UserModel { get; set; }

    public RoomModel()
    {
    }
}