using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.DataAcces.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            //_db.Product.Update(product);
            var objectFromDb = _db.Product.FirstOrDefault(u => u.Id == product.Id);
            if (objectFromDb != null)
            {
                objectFromDb.Brand = product.Brand;
                objectFromDb.CarModel = product.CarModel;
                objectFromDb.KilometresDriven = product.KilometresDriven;
                objectFromDb.Price = product.Price;
                objectFromDb.PowerInKilowatts = product.PowerInKilowatts;
                objectFromDb.Description = product.Description;
                objectFromDb.CategoryId = product.CategoryId;
                objectFromDb.Category = product.Category;

                if(product.ImageUrl != null)
                {
                    objectFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
