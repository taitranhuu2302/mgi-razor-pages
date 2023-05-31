using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models.User;

namespace movie.Pages;

public class Login : PageModel
{
    [BindProperty] public UserLogin UserLogin { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var check = FakeData.Users.FirstOrDefault(u => u.Email == UserLogin.Email && u.Password == UserLogin.Password);
        if (check == null) return Page();

        HttpContext.Session.SetString("User", check.Id);
    
        return Redirect("/");
    }
}