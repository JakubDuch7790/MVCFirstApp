using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Areas.Host.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
