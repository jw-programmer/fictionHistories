using System.Collections.Generic;
using System.Threading.Tasks;
using Src.Queries;

namespace Src.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IList<T>> GetAllAsync(PaginationQuery paginationQuery);
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(T obj);
    }
}