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

    public IActionResult OnGet(PaginateModel paginateModel)
    {
        this.PaginateModel = paginateModel;
        var rooms = Enumerable.Reverse(FakeData.Rooms).ToList();
        this.PaginateModel.Count = rooms.Count;
        rooms = rooms.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        this.Rooms = rooms;
        return Page();
    }

    public IActionResult OnPostCreate(RoomCreate request)
    {
        FakeData.Rooms.Add(new RoomModel()
        {
            Name = request.Name
        });
        return RedirectToPage("Room");
    }

    public IActionResult OnPostEdit(RoomEdit request)
    {
        var room = FakeData.Rooms.FirstOrDefault(r => r.Id == request.Id);
        if (room != null)
        {
            room.Name = request.Name ?? room.Name;
        }

        return RedirectToPage("Room");
    }

    public IActionResult OnPostRemove(string roomId)
    {
        var room = FakeData.Rooms.FirstOrDefault(r => r.Id == roomId);

        if (room != null)
        {
            FakeData.Rooms.Remove(room);
        }

        return RedirectToPage("Room");
    }
}