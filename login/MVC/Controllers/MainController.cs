using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace login.MVC.Controllers;

[Route("Home")]
public class MainController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}