using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using MVCFirstApp.Utility;
using System.Diagnostics;
using System.Security.Claims;


namespace MVCFirstApp.Areas.Admin.Controllers;

[Area("Admin")]
    public class OrderController : Controller

{
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index() 
    {
        return View();
    }
    public IActionResult Details(int orderId)
    {
        OrderVM orderVM= new()
        {
            OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includedProperties: "ApplicationUser"),
            OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includedProperties:"Product")
        };
        return View(orderVM);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<OrderHeader> objOrderHeaders;

        if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
        {
            objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includedProperties: "ApplicationUser").ToList();
        }
        else
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            objOrderHeaders = _unitOfWork.OrderHeader
                .GetAll(u => u.ApplicationUserId == userId, includedProperties: "ApplicationUser");
        }


        IEnumerable<OrderHeader> objOrderList = _unitOfWork.OrderHeader.GetAll(includedProperties: "ApplicationUser").ToList();

        switch (status)
        {
            case "pending":
                objOrderList = objOrderList.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                break;
            case "inprocess":
                objOrderList = objOrderList.Where(u => u.OrderStatus == SD.StatusInProcess);
                break;
            case "completed":
                objOrderList = objOrderList.Where(u => u.PaymentStatus == SD.StatusShipped);
                break;
            case "approved":
                objOrderList = objOrderList.Where(u => u.PaymentStatus == SD.StatusApproved);
                break;
            default:
                break;
        }
        return Json(new { data = objOrderList });
    }

    #endregion
}
