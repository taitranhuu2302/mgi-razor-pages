using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models;
using movie.Models.Movie;
using movie.Models.Ticket;

namespace movie.Pages;

public class Movie : PageModel
{
    [BindProperty] public List<MovieModel> Movies { get; set; } = new List<MovieModel>();
    [BindProperty] public PaginateModel PaginateModel { get; set; } = null!;

    public IActionResult OnGet(PaginateModel paginateModel)
    {
        this.PaginateModel = paginateModel;
        var movies = Enumerable.Reverse(FakeData.Movies).ToList();
        this.PaginateModel.Count = movies.Count;
        movies = movies.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        this.Movies = movies;
        return Page();
    }
    
    public IActionResult OnGetData(PaginateModel paginateModel)
    {
        this.PaginateModel = paginateModel;
        var movies = Enumerable.Reverse(FakeData.Movies).ToList();
        this.PaginateModel.Count = movies.Count;
        movies = movies.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        this.Movies = movies;
        return new JsonResult(movies);
    }

    public IActionResult OnPostCreate(MovieCreate request)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Create", "Create movie error");
            this.Movies = Enumerable.Reverse(FakeData.Movies).ToList();
            return Page();
        }

        var room = FakeData.Rooms.FirstOrDefault(r => r.Id == request.RoomId)!;

        var movie = new MovieModel()
        {
            Name = request.Name,
            Price = request.Price,
            Image = request.Image
        };
        for (int i = 0; i < request.TicketCount; i++)
        {
            movie.Tickets.Add(new TicketModel()
            {
                Movie = movie,
                Room = room
            });
        }

        FakeData.Movies.Add(movie);

        return RedirectToPage("Movie");
    }

    public IActionResult OnPostEdit(MovieEdit request)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Edit", "Edit movie error");
            this.Movies = Enumerable.Reverse(FakeData.Movies).ToList();
            return Page();
        }
        var movie = FakeData.Movies.FirstOrDefault(m => m.Id == request.Id);
        if (movie != null)
        {
            movie.Name = request.Name;
            movie.Image = request.Image;
            movie.Price = request.Price;
        }

        return RedirectToPage("Movie");
    }

    public IActionResult OnPostRemove(string id)
    {
        var movie = FakeData.Movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            FakeData.Movies.Remove(movie);
        }

        return RedirectToPage("Movie");
    }
}