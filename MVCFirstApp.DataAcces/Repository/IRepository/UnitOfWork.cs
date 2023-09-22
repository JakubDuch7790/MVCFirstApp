using MVCFirstApp.DataAcces.Data;
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
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
