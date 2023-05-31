namespace movie.Models.User;

public class UserCreate
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public string? Day { get; set; } = null!;
    public string? Month { get; set; } = null!;
    public string? Year { get; set; } = null!;
}