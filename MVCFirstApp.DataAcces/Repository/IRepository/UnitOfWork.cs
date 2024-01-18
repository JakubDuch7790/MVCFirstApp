using Microsoft.EntityFrameworkCore;
using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.DataAcces.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get;private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void SetIdentityInsertON()
        {
            _db.Database.BeginTransaction();
            _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ShoppingCarts ON");
        }
        public void SetIdentityInsertOFF()
        {

            _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ShoppingCarts OFF");
            _db.Database.CommitTransaction();
        }
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            //var newArray = 
            var newList = new List<int>();
            int k = 0;

            for (int i = 0; i < nums1.Length; i++)
            {
                newList.Add(nums1[i]);
                if (i < nums2.Length - 1)
                {
                    k++;
                }
                else
                {

                }

            }
            newList.ToArray();


        }

    }
}
