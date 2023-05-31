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
            
        this.PaginateModel = paginateModel;
        var tickets = Enumerable.Reverse(user.Tickets).ToList();
        this.PaginateModel.Count = tickets.Count;
        tickets = tickets.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        this.UserModel = user;
        this.TicketModels = tickets;

        return Page();
    }

    public IActionResult OnPostRemoveMovieBooked(string movieId)
    {
        string? id = HttpContext.Session.GetString("User");
        if (id == null) return Redirect("/Auth");

        UserModel userModel = FakeData.Users.FirstOrDefault(u => u.Id == id)!;

        MovieModel? movie = FakeData.Movies.FirstOrDefault(m => m.Id == movieId);
        TicketModel? ticketCheck = userModel.Tickets.FirstOrDefault(m => m.Movie.Id == movieId);

        if (movie != null && ticketCheck != null)
        {
            movie.Tickets.Add(ticketCheck);
            userModel.Tickets.Remove(ticketCheck);
        }

        return RedirectToPage("Profile");
    }
}