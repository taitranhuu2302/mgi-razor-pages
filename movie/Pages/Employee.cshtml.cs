using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movie.Data;
using movie.Models;
using movie.Models.User;

namespace movie.Pages;

public class Employee : PageModel
{
    [BindProperty] public List<UserModel> ListUser { get; set; } = new List<UserModel>();
    [BindProperty] public UserModel? User { get; set; } = null!;
    [BindProperty] public PaginateModel PaginateModel { get; set; } = null!;

    public IActionResult OnGet(PaginateModel paginateModel)
    {
        var user = FakeData.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetString("User"));
        if (user == null)
        {
            return RedirectToPage("Login");
        }

        this.PaginateModel = paginateModel;
        var listUser = Enumerable.Reverse(FakeData.Users).Where(u => u.Id != user.Id).ToList();
        this.PaginateModel.Count = listUser.Count;
        listUser = listUser.Skip((paginateModel.CurrentPage - 1) * paginateModel.PageSize)
            .Take(paginateModel.PageSize).ToList();
        this.ListUser = listUser;
        this.User = user;
        return Page();
    }

    public IActionResult OnPostCreate(UserCreate request)
    {
        if (!ModelState.IsValid)
        {
            var current = FakeData.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetString("User"));
            this.ListUser = Enumerable.Reverse(FakeData.Users).Where(u => u.Id != current?.Id).ToList();
            this.User = current;
            ModelState.AddModelError("Create", "Error");

            return Page();
        }

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

        return RedirectToPage("Employee");
    }

    public IActionResult OnPostEdit(UserEdit request)
    {
        if (!ModelState.IsValid)
        {
            var current = FakeData.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetString("User"));
            this.ListUser = Enumerable.Reverse(FakeData.Users).Where(u => u.Id != current?.Id).ToList();
            this.User = current;
            ModelState.AddModelError("Edit", "Error");
            return Page();
        }
        
        var user = FakeData.Users.FirstOrDefault(u => u.Id == request.Id);
        if (user != null)
        {
            user.Email = request.Email ?? user.Email;
            user.Name = request.Name ?? user.Name;
            user.Password = request.Password ?? user.Password;
            user.Day = request.Day ?? user.Day;
            user.Month = request.Month ?? user.Month;
            user.Year = request.Year ?? user.Year;
        }

        return RedirectToPage("Employee");
    }

    public IActionResult OnPostRemove(string id)
    {
        var user = FakeData.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            FakeData.Users.Remove(user);
        }

        return RedirectToPage("Employee");
    }
}