using System.ComponentModel.DataAnnotations;

namespace movie.Models.User;

public class UserLogin
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}