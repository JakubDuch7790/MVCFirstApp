using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;


namespace MVCFirstApp.Areas.Admin.Controllers
{
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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeader> objOrderList = _unitOfWork.OrderHeader.GetAll(includedProperties: "ApplicationUser").ToList();

            //var modifiedOrderList = new List<object>();
            return Json(new { data = objOrderList });
        }

        #endregion
    }
}
