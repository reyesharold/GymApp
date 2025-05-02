using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public interface ICommonRepo<T> where T : class
    {
        Task<T> AddAync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> condition,Func<IQueryable<T>, IQueryable<T>> query);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? condition = null, Func<IQueryable<T>, IQueryable<T>>? query = null);
        Task<bool> DeleteViaIdAsync(int id);
        Task<T> UpdateAsync(T entity, params Expression<Func<T, object>>[] updateProperties);
    
    }
}
