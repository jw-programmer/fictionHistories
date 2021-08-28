using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Src.Data;
using Src.Queries;

namespace Src.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FictionDbContext _context = null;
        private readonly DbSet<T> table = null;
        
        public GenericRepository(FictionDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task DeleteAsync(T obj)
        {
            table.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync(PaginationQuery paginationQuery)
        {
            if(paginationQuery == null)
            {
                return await table.ToListAsync();
            }
            return await table
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
            .Take(paginationQuery.PageSize)
            .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public virtual async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}