using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Src.Data;
using Src.Models;

namespace Src.Repositories
{
    public class ChapterRepository : GenericRepository<Chapter>
    {
        private readonly FictionDbContext _context;
        public ChapterRepository(FictionDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IList<Chapter>> GetByHistoryIdAsync(int id)
        {
            return await _context.Chapters.Where(c => c.HistoryId == id).ToListAsync();
        }

        public override Task InsertAsync(Chapter obj)
        {
            int index = _context.Chapters.Include(c => c.History).ThenInclude(h => h.Chapters).Select(c => c.History.Chapters.Count).First();
            obj.Index = index;
            return base.InsertAsync(obj);
        }
    }
}