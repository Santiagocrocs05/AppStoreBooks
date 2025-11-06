using AppStore.Models.Domain;
using AppStore.Models.DTOs;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Repositories.Implementation;


public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Status> LoginAsync(LoginModel login)
    {
        var status = new Status();
        var user = await _userManager.FindByNameAsync(login.Username!);
        if (user is null)
        {
            status.StatusCode = 0;
            status.Message = "Invalid username.";
            return status;
        }
        var passvalidation = await _userManager.CheckPasswordAsync(user, login.Password!);
        if (!passvalidation)
        {
            status.StatusCode = 0;
            status.Message = "Invalid  password.";
            return status;
        }

        var result = await _signInManager.PasswordSignInAsync(user, login.Password!, true, false);
        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "Invalid username or password.";
            return status;
        }
        status.StatusCode = 1;
        status.Message = "Login successful.";
        return status;
    }

    public async Task<bool> LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return true;
        }
        catch (System.Exception)
        {
            return false;

        }

    }
}