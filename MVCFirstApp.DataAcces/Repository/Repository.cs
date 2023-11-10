using Microsoft.EntityFrameworkCore;
using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.DataAcces.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.DataAcces.Repository
{
    public class Repository<T> : IRepository<T> where T : class

    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet
            _db.Product.Include(u => u.Category)/*.Include(i => i.Price)*/;
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includedProperties)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includedProperties))
            {
                foreach (var includedProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includedProperty);
                }
            }
            return query.FirstOrDefault();
        }
        public IEnumerable<T> GetAll(string? includedProperties)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includedProperties))
            {
                foreach(var includedProperty in includedProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includedProperty);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
