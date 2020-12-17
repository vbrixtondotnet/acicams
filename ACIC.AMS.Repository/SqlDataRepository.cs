using ACIC.AMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ACIC.AMS.Repository
{
    public class SqlDataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ACICDBContext _context;

        public SqlDataRepository(ACICDBContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChangesAsync();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll(string[] childEntities = null)
        {
            if (childEntities != null)
            {
                IQueryable<T> query = _context.Set<T>();
                if (childEntities != null)
                {
                    foreach (string entity in childEntities)
                    {
                        query = query.Include(entity);

                    }
                }
            }
            return _context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate, string[] childEntities = null)
        {

            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (childEntities != null)
                {
                    foreach (string entity in childEntities)
                    {
                        query = query.Include(entity);

                    }
                }

                return query.Where(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<T> Include(params Expression<Func<T, object>>[] includeExpressions)
        {
            DbSet<T> dbSet = _context.Set<T>();

            IQueryable<T> query = null;
            foreach (var includeExpression in includeExpressions)
            {
                query = dbSet.Include(includeExpression);
            }

            return query ?? dbSet;
        }

    }
}
