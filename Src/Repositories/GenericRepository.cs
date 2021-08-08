using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Src.Data;

namespace Src.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private FictionDbContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            _context = new FictionDbContext();
            table = _context.Set<T>();
        }

        public GenericRepository(FictionDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task deleteAsync(T obj)
        {
            table.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> getAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task insertAsync(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task updateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}