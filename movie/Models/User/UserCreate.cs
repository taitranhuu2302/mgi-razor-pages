using System.ComponentModel.DataAnnotations;

namespace movie.Models.User;

public class UserCreate
{
    [Required] public string Name { get; set; } = null!;
    [Required] [EmailAddress] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string Day { get; set; } = null!;
    [Required] public string Month { get; set; } = null!;
    [Required] public string Year { get; set; } = null!;
}