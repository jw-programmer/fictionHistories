using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Src.Queries;

namespace Src.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll(PaginationQuery paginationQuery);
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(T obj);
    }
}