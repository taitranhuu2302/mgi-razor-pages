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

}