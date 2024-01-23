using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Areas.Host.Controllers;

[Area("Host")]
public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
