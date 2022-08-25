using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticate _authentication;

    public AccountController(IAuthenticate authentication)
    {
        _authentication = authentication;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authentication.AuthenticateAsync(email: model.Email, password: model.Password);
        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");

            return RedirectToAction(model.ReturnUrl);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt. Password must be strong.");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authentication.AuthenticateAsync(email: model.Email, password: model.Password);
        if (result)
        {
            return Redirect("/");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt. Password must be strong.");
            return View(model);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authentication.LogoutAsync();
        return Redirect("/Account/Login");
    }

    public IActionResult Index() => View();
}
