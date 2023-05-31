using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models.User;

namespace movie.Pages;

public class Admin : PageModel
{
    public IActionResult OnGet()
    {
        string? id = HttpContext.Session.GetString("User");
        UserModel? user = FakeData.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return RedirectToPage("Login");
        
        return Page();
    }
}