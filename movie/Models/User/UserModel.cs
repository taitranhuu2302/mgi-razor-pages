using movie.Models.Ticket;

namespace movie.Models.User;

public enum UserRole
{
    Admin,
    User
}

public class UserModel : BaseModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    
    public string? Day { get; set; } = null!;
    public string? Month { get; set; } = null!;
    public string? Year { get; set; } = null!;

    public List<TicketModel> Tickets = new List<TicketModel>();

    public UserModel(string name, string email, string password, UserRole role)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Role = role;
    }

    public UserModel()
    {
    }
}