using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using MVCFirstApp.Utility;
using System.Data;

namespace MVCFirstApp.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
    public IActionResult Index()
        {
        List<Product> objCategoryList = _unitOfWork.Product.GetAll(includedProperties:"Category").ToList();

        return View(objCategoryList);
        }

    public IActionResult Upsert(int? id)
        {

        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(c =>

            new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
            Product = new Product()
            };


        if(id == null || id == 0)
        {
            //create
            return View(productVM);
        }
        else
        {
            //update
            productVM.Product = _unitOfWork.Product.Get(product => product.Id == id);
            return View(productVM);
        }

        }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if(file!= null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    //delete old img
                    var oldImgPath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }
            
            if (productVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product successfully created";
            return RedirectToAction("Index");
        }
        else
        {
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(c =>

                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });
            };

            return View(productVM);
        }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objCategoryList = _unitOfWork.Product.GetAll(includedProperties: "Category").ToList();
        return Json(new { data = objCategoryList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToDelete = _unitOfWork.Product.Get(p =>  p.Id == id);

        if (productToDelete == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImgPath))
        {
            System.IO.File.Delete(oldImgPath);
        }

        _unitOfWork.Product.Remove(productToDelete);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}



