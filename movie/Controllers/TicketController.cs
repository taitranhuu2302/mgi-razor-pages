using Microsoft.AspNetCore.Mvc;
using movie.Data;
using movie.Models;
using movie.Models.Movie;
using movie.Models.Ticket;
using movie.Models.User;

namespace movie.Controllers;

[ApiController]
[Route("/api/ticket")]
public class TicketController : ControllerBase
{
    // GET
    [HttpGet]
    public PaginateResponse<TicketModel> Index([FromQuery] PaginateModel paginateModel)
    {
        string? id = HttpContext.Session.GetString("User");
        UserModel? user = FakeData.Users.FirstOrDefault(u => u.Id == id);

        var tickets = Enumerable.Reverse(user?.Tickets).ToList();
        paginateModel.Count = tickets.Count;
        tickets = tickets.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();

        return new PaginateResponse<TicketModel>
        {
            Data = tickets,
            Count = paginateModel.Count,
            PageSize = paginateModel.PageSize,
            CurrentPage = paginateModel.CurrentPage,
        };
    }

    [HttpDelete("{ticketId}")]
    public IActionResult OnPostRemoveMovieBooked([FromRoute] string ticketId)
    {
        string? id = HttpContext.Session.GetString("User");

        UserModel userModel = FakeData.Users.FirstOrDefault(u => u.Id == id)!;

        TicketModel? ticketCheck = userModel.Tickets.FirstOrDefault(m => m.Id == ticketId);
        MovieModel? movie = FakeData.Movies.FirstOrDefault(m => m.Id == ticketCheck?.Movie.Id);

        if (movie != null && ticketCheck != null)
        {
            movie.Tickets.Add(ticketCheck);
            userModel.Tickets.Remove(ticketCheck);
        }

        return new JsonResult("");
    }
}