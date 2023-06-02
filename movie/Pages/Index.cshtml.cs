using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models.Movie;
using movie.Models.User;

namespace movie.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public List<MovieModel> Movies;
    public UserModel? UserModel;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        string? id = HttpContext.Session.GetString("User");
        UserModel? user = FakeData.Users.FirstOrDefault(u => u.Id == id);
        this.Movies = Enumerable.Reverse(FakeData.Movies).ToList();
        this.UserModel = FakeData.Users.FirstOrDefault(u => u.Id == id);
    }

    public IActionResult OnPostMovieBooked(string movieId)
    {
        string? id = HttpContext.Session.GetString("User");
        if (id == null) return Redirect("/Login");

        UserModel userModel = FakeData.Users.FirstOrDefault(u => u.Id == id)!;

        MovieModel? movie = FakeData.Movies.FirstOrDefault(m => m.Id == movieId);
        var check = userModel.Tickets.FirstOrDefault(t => t.Movie.Id == movieId);

        if (movie != null && check == null && movie.Tickets.Count > 0)
        {
            var ticket = movie.Tickets.FirstOrDefault()!;
            userModel.Tickets.Add(ticket);
            movie.Tickets.Remove(ticket);
        }

        return RedirectToPage("Profile");
    }
}