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

    public void OnGet()
    {
        
    }
    

}