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
        public ICategoryRepository CategoryRepository { get;private set; }
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db, CategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
