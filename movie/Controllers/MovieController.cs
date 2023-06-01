using Microsoft.AspNetCore.Mvc;
using movie.Data;
using movie.Models.Movie;

namespace movie.Controllers;

[ApiController]
[Route("/api/movies")]
public class MovieController : ControllerBase
{
    // GET
    public List<MovieModel> Index()
    {
        return FakeData.Movies;
    }
}