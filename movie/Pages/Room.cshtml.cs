using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models;
using movie.Models.Room;

namespace movie.Pages;

public class Room : PageModel
{
    [BindProperty] public List<RoomModel> Rooms { get; set; } = new List<RoomModel>();
    [BindProperty] public PaginateModel PaginateModel { get; set; } = null!;

    public void OnGet()
    {
    }

    
}