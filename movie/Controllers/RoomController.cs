using Microsoft.AspNetCore.Mvc;
using movie.Data;
using movie.Models;
using movie.Models.Movie;
using movie.Models.Room;

namespace movie.Controllers;

[ApiController]
[Route("/api/rooms")]
public class RoomController : ControllerBase
{
    [HttpGet]
    public PaginateResponse<RoomModel> Index([FromQuery] PaginateModel paginateModel)
    {
        var rooms = Enumerable.Reverse(FakeData.Rooms).ToList();
        paginateModel.Count = rooms.Count;
        rooms = rooms.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        return new PaginateResponse<RoomModel>()
        {
            Data = rooms,
            Count = paginateModel.Count,
            PageSize = paginateModel.PageSize,
            CurrentPage = paginateModel.CurrentPage,
        };
    }
    
    [HttpPost]
    public IActionResult OnPostCreate([FromBody] RoomCreate request)
    {
        FakeData.Rooms.Add(new RoomModel()
        {
            Name = request.Name,
            Seats = request.Seats
        });
        return new JsonResult("");
    }

    [HttpPut]
    public IActionResult OnPostEdit([FromBody] RoomEdit request)
    {
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

        return new JsonResult("");
    }

    [HttpDelete("{id}")]
    public IActionResult OnPostRemove([FromRoute] string id)
    {
        var room = FakeData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room != null)
        {
            FakeData.Rooms.Remove(room);
        }

        return new JsonResult("");
    }
}