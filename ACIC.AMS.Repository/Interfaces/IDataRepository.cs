using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ACIC.AMS.Repository.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        void Create(T entity);
        void CreateAsync(T entity);

        void CreateRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateAsync(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        T Get(int id);

        Task<T> GetAsync(int id);

        IEnumerable<T> GetAll(string[] childEntities = null);

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate, string[] childEntities = null);

        IEnumerable<T> Include(params Expression<Func<T, object>>[] includeExpressions);
    }
}
