using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models;
using movie.Models.Movie;
using movie.Models.Ticket;
using movie.Models.User;

namespace movie.Pages;

public class Profile : PageModel
{
    [BindProperty] public UserModel UserModel { get; set; } = null!;
    [BindProperty] public List<TicketModel> TicketModels { get; set; } = null!;
    [BindProperty] public PaginateModel PaginateModel { get; set; } = null!;

    public IActionResult OnGet(PaginateModel paginateModel)
    {
        string? id = HttpContext.Session.GetString("User");
        UserModel? user = FakeData.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return RedirectToPage("Login");
            
        this.UserModel = user;

        return Page();
    }

    
}