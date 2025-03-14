using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {



        private readonly ApplicationDbContext _db;
        // private readonly DbSet<T> _dbSet;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet = _db.Set<T>();
            // _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            // throw new NotImplementedException();
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
            // throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            // throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}