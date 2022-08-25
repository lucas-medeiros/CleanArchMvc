using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Privacy() => View();
}
