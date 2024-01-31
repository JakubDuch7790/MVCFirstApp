using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using MVCFirstApp.Utility;
using System.Security.Claims;

namespace MVCFirstApp.Areas.Host.Controllers;

[Area("Host")]
[Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

	[BindProperty]
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
            OrderHeader = new()
        };

        foreach(var cart in ShoppingCartVM.ShoppingCartList)
        {
            cart.Price = cart.Product.Price;

            ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
        }

        return View(ShoppingCartVM);
    }

    public IActionResult Remove(int cartId) {
        var cartToRemove = _unitOfWork.ShoppingCart.Get(u => u.Id== cartId);
        _unitOfWork.ShoppingCart.Remove(cartToRemove);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

	public IActionResult Summary()
	{
		var claimsIdentity = (ClaimsIdentity)this.User.Identity;
		var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		ShoppingCartVM = new()
		{
			ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includedProperties: "Product"),
			OrderHeader = new()
		};

		ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

		ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
		ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
		ShoppingCartVM.OrderHeader.Country = ShoppingCartVM.OrderHeader.ApplicationUser.Country;
		ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
		ShoppingCartVM.OrderHeader.StreetAdress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAdress;


		foreach (var cart in ShoppingCartVM.ShoppingCartList)
		{
			cart.Price = cart.Product.Price;

			ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
		}

		return View(ShoppingCartVM);
	}
	[HttpPost]
	[ActionName("Summary")]
	public IActionResult SummaryPOST(ShoppingCartVM shoppingCartVM)
	{
		var claimsIdentity = (ClaimsIdentity)this.User.Identity;
		var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includedProperties: "Product");

		ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

		ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
		ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

		//ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u =>u.Id == userId);

		foreach (var cart in ShoppingCartVM.ShoppingCartList)
		{
			cart.Price = cart.Product.Price;

			ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
		}

		if(applicationUser.CompanyId.GetValueOrDefault() == 0)
		{
			//Regular Customer
			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
		}
		else
		{
			//Company
			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
		}

		//ModelState.Clear();

		_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
		_unitOfWork.Save();

		foreach(var cart in ShoppingCartVM.ShoppingCartList)
		{
			OrderDetail orderDetail = new()
			{
				ProductId = cart.ProductId,
				OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
				Price = cart.Price,
			};

			_unitOfWork.OrderDetail.Add(orderDetail);
			_unitOfWork.Save();
		}

		if (applicationUser.CompanyId.GetValueOrDefault() == 0)
		{
			//Regular Customer - Payment Required - Transaction(Stripe) Logic
		}



		return RedirectToAction(nameof(OrderConfirm), new { id = ShoppingCartVM.OrderHeader.Id});
	}

	public IActionResult OrderConfirm(int id)
	{
		return View(id);
	}


}
