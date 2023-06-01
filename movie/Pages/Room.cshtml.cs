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
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Create", "Create room error");
            this.Rooms = Enumerable.Reverse(FakeData.Rooms).ToList();
            return Page();
        }
        FakeData.Rooms.Add(new RoomModel()
        {
            Name = request.Name,
            Seats = request.Seats
        });
        return RedirectToPage("Room");
    }

    public IActionResult OnPostEdit(RoomEdit request)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Edit", "Edit room error");
            this.Rooms = Enumerable.Reverse(FakeData.Rooms).ToList();
            return Page();
        }
        
        var room = FakeData.Rooms.FirstOrDefault(r => r.Id == request.Id);
        if (room != null)
        {
            var user = FakeData.Users.FirstOrDefault(u => u.Id == request.UserId);
            room.Name = request.Name ?? room.Name;
            room.Seats = request.Seats;
            room.RoomStatus = request.RoomStatus;
            room.StartTime = request.StartTime ?? room.StartTime;
            room.EndTime = request.EndTime ?? room.EndTime;
            room.UserModel = user ?? room.UserModel;
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