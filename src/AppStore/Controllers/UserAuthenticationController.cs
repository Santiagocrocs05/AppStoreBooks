using AppStore.Models.DTOs;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers;

public class UserAuthenticationController : Controller
{
    private readonly IUserAuthenticationService _authService;

    public UserAuthenticationController(IUserAuthenticationService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginModel);
        }

        var result = await _authService.LoginAsync(loginModel);

        if (result.StatusCode == 1)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["Msg"] = result.Message;
            return RedirectToAction(nameof(Login));
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
}
