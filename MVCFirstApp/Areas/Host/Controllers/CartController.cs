using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using MVCFirstApp.Utility;
using Stripe.Checkout;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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
        ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;


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


		ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
		ShoppingCartVM.OrderHeader.ApplicationUserId = userId;


		//ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

		//ApplicationUser applicationUser = _unitOfWork.ApplicationUser
		//.Get(u => u.Id == userId, includedProperties: "ShoppingCart");

		//if (applicationUser != null)
		//{
		//    _unitOfWork.Attach(applicationUser);
		//}

		ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, tracked:true);

		foreach (var cart in ShoppingCartVM.ShoppingCartList)
		{
			cart.Price = cart.Product.Price;

			ShoppingCartVM.OrderHeader.OrderTotal += cart.Price;
		}

		if(ShoppingCartVM.OrderHeader.ApplicationUser.CompanyId.GetValueOrDefault() == 0)
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

		if (ShoppingCartVM.OrderHeader.ApplicationUser.CompanyId.GetValueOrDefault() == 0)
		{
			//Regular Customer - Payment Required - Transaction(Stripe) Logic

			var domain = "https://localhost:7256/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain+ $"host/cart/OrderConfirm?id={ShoppingCartVM.OrderHeader.Id}",
				CancelUrl = domain+ $"customer/cart/Index",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

			foreach(var item in  ShoppingCartVM.ShoppingCartList)
			{
				var sessionLineItem = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(item.Price * 100), //$20.50 => 2050
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.Product.CarModel,
						}
					},
					Quantity = 1
				};
				options.LineItems.Add(sessionLineItem);
            }


            var service = new Stripe.Checkout.SessionService();
            Session session = service.Create(options);

			_unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
			_unitOfWork.Save();

			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);
        }
		return RedirectToAction(nameof(OrderConfirm), new { id = ShoppingCartVM.OrderHeader.Id});
	}

	public IActionResult OrderConfirm(int id)
	{
		OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(oh => oh.Id == id, includedProperties: "ApplicationUser");

		if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
		{
			//regular order by customer
			var service = new SessionService();
			Session session = service.Get(orderHeader.SessionId);

			if (session.PaymentStatus.ToLower() == "paid")
			{
				_unitOfWork.OrderHeader.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
				_unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
				_unitOfWork.Save();
			}
		}

		List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart
			.GetAll(filter: sc => sc.ApplicationUserId == orderHeader.ApplicationUserId,
					includedProperties: "ApplicationUser")
			.ToList();

		_unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
		_unitOfWork.Save();

        return View(id);
    }
	private void CreateShoppingVM()
	{

	}


}
