﻿using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.DataAcces.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Category.Update(category);
        }
    }
}
