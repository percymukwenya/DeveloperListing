using ProjectManagement.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedList<T>> Find(UserParams userParams);
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<bool> Delete(int id);
        Task<T> Upsert(T entity);
        Task<IEnumerable<T>> All();
    }
}
