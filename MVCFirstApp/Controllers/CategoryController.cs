using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
