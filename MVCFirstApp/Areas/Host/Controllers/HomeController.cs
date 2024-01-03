using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCFirstApp.Areas.Host.Controllers
{
    [Area("Host")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includedProperties: "Category");

            return View(products);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = _unitOfWork.Product.Get(p => p.Id == id, includedProperties: "Category"),
                Count = 1,
                ProductId = id
            };

            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = userId;

            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();

            return View(shoppingCart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}