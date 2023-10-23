using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;

namespace MVCFirstApp.Areas.Admin.Controllers;

[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    public IActionResult Index()
        {
        List<Product> objCategoryList = _unitOfWork.Product.GetAll().ToList();

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
                })
            };

        if(id == null && id == 0)
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
    public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
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
    public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? wantedProductfromDB = _unitOfWork.Product.Get(ID => ID.Id == id);
            //Category? wantedCategoryfromDB1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? wantedCategoryfromDB2 = _db.Categories.Where(c=> c.Id == id).FirstOrDefault();

            if (wantedProductfromDB == null)
            {
                return NotFound();
            }

            return View(wantedProductfromDB);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Poduct successfully updated";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? wantedProductfromDB = _unitOfWork.Product.Get(ID => ID.Id == id);

            if (wantedProductfromDB == null)
            {
                return NotFound();
            }

            return View(wantedProductfromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(ID => ID.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product successfully removed";

            return RedirectToAction("Index");
        }
    }



