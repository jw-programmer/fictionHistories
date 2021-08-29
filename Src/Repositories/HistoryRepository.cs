using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Src.Data;
using Src.Models;

namespace Src.Repositories
{
    public class HistoryRepository : GenericRepository<History>
    {
        private readonly FictionDbContext _context;
        public HistoryRepository(FictionDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override Task<History> GetByIdAsync(int id)
        {
            return _context.Histories.Include(h => h.Genres)
            .Include(h => h.Author)
            .FirstOrDefaultAsync(h => h.Id == id);
        }

        public override async Task InsertAsync(History obj)
        {
            _context.Genres.AttachRange(obj.Genres);
            obj.PublishDate = DateTime.Now;
            await base.InsertAsync(obj);
        }

        public async Task<IList<History>> GetByGenre(Genre genre){
            return await _context.Histories.Where(x => x.Genres.Contains(genre)).ToListAsync();
        }

        public async Task<IList<History>> GetByTitle(string title)
        {
            return await _context.Histories.Where(x => x.Title.Contains(title)).ToListAsync();
        }

        public async Task UpdateGenresAsync(int id, Genre obj)
        {
            var history = await _context.Histories.Include(h => h.Genres).FirstOrDefaultAsync(h => h.Id == id);
            var genre = history.Genres.Where(g => g.Id == obj.Id).First();
            history.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}