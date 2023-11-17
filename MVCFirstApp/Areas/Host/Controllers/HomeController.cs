﻿using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using System.Diagnostics;

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
            Product product = _unitOfWork.Product.Get(p => p.Id==id, includedProperties: "Category");

            return View(product);
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