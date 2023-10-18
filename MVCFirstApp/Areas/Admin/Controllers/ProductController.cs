using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;

namespace MVCFirstApp.Areas.Admin.Controllers
{
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

            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c =>
            //new SelectListItem
            //{
            //    Text = c.Name,
            //    Value = c.Id.ToString(),
            //});

            return View(objCategoryList);
            }

        public IActionResult Create()
            {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c =>
            new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });

            //ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;

            return View();
            }

        [HttpPost]
        public IActionResult Create(Product obj)
            {
                //if (obj.Brand != null && obj.Brand.ToLower() == obj..ToString())
                //{
                //    ModelState.AddModelError("name", "Property name cannot be same as DisplayOrder");
                //}
                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product successfully created";
                    return RedirectToAction("Index");
                }
                return View();
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

    }

