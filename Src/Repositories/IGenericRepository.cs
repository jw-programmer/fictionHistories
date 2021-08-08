using System.Collections.Generic;
using System.Threading.Tasks;

namespace Src.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IList<T>> getAllAsync();
        Task<T> getByIdAsync(int id);
        Task insertAsync(T obj);
        Task updateAsync(T obj);
        Task deleteAsync(T obj);
    }
}