using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using System.Security.Claims;

namespace MVCFirstApp.Areas.Host.Controllers;

[Area("Host")]
[Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ShoppingCartVM ShoppingCartVM { get; set; }
    public CartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)this.User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new()
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includedProperties: "Product"),
        };

        foreach(var cart in ShoppingCartVM.ShoppingCartList)
        {
            cart.Price = cart.Product.Price;

            ShoppingCartVM.OrderTotal += cart.Price;
        }

        return View(ShoppingCartVM);
    }

    //private double GetPrice(ShoppingCart cart)
    //{
    //    return cart.Product.Price;
    //}


}
