using Microsoft.AspNetCore.Mvc;

namespace wt_lab4_4mvc_denisenko.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
