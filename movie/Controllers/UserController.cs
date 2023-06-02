using Microsoft.AspNetCore.Mvc;
using movie.Data;
using movie.Models;
using movie.Models.User;
using Newtonsoft.Json;

namespace movie.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    [HttpGet]
    public PaginateResponse<UserModel> Index([FromQuery] PaginateModel paginateModel)
    {
        var user = FakeData.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetString("User"));
        var listUser = Enumerable.Reverse(FakeData.Users).Where(u => u.Id != user?.Id).ToList();
        paginateModel.Count = listUser.Count;
        listUser = listUser.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();
        return new PaginateResponse<UserModel>()
        {
            Data = listUser,
            Count = paginateModel.Count,
            PageSize = paginateModel.PageSize,
            CurrentPage = paginateModel.CurrentPage,
        };
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] UserCreate request)
    {
        var user = new UserModel()
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
            Day = request.Day,
            Month = request.Month,
            Year = request.Year
        };

        FakeData.Users.Add(user);

        return new JsonResult("");
    }

    [HttpPut]
    public IActionResult OnPostEdit(UserEdit request)
    {
        var user = FakeData.Users.FirstOrDefault(u => u.Id == request.Id);
        if (user != null)
        {
            user.Email = request.Email;
            user.Name = request.Name;
            user.Password = request.Password;
            user.Day = request.Day ?? user.Day;
            user.Month = request.Month ?? user.Month;
            user.Year = request.Year ?? user.Year;
        }

        return new JsonResult("");
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult OnPostRemove([FromRoute] string id)
    {
        var user = FakeData.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            FakeData.Users.Remove(user);
        }

        return new JsonResult("");
    }
}