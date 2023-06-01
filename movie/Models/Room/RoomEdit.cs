using movie.Models.User;

namespace movie.Models.Room;

public class RoomEdit
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public int Seats { get; set; }
    public RoomStatus RoomStatus { get; set; } = RoomStatus.Available;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string UserId { get; set; } = null!;
}