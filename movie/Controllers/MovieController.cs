using Microsoft.AspNetCore.Mvc;
using movie.Data;
using movie.Models;
using movie.Models.Movie;
using movie.Models.Ticket;

namespace movie.Controllers;

[ApiController]
[Route("/api/movies")]
public class MovieController : ControllerBase
{
    [HttpGet]
    public PaginateResponse<MovieModel> Index([FromQuery] PaginateModel paginateModel)
    {
        var movies = Enumerable.Reverse(FakeData.Movies).ToList();
        paginateModel.Count = movies.Count;
        movies = movies.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        return new PaginateResponse<MovieModel>()
        {
            Data = movies,
            Count = paginateModel.Count,
            PageSize = paginateModel.PageSize,
            CurrentPage = paginateModel.CurrentPage,
        };
    }

    [HttpPost]
    public IActionResult OnPostCreate([FromBody] MovieCreate request)
    {
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

        return new JsonResult("");
    }

    [HttpPut]
    public IActionResult OnPostEdit([FromBody]MovieEdit request)
    {
        var movie = FakeData.Movies.FirstOrDefault(m => m.Id == request.Id);
        if (movie != null)
        {
            movie.Name = request.Name;
            movie.Image = request.Image;
            movie.Price = request.Price;
        }

        return new JsonResult("");
    }

    [HttpDelete("{id}")]
    public IActionResult OnPostRemove([FromRoute] string id)
    {
        var movie = FakeData.Movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            FakeData.Movies.Remove(movie);
        }

        return new JsonResult("");
    }
}