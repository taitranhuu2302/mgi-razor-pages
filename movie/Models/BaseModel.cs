namespace movie.Models;

public abstract class BaseModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt = new DateTime();
    public DateTime UpdatedAt = new DateTime();
}