using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Src.Data;
using Src.Models;

namespace Src.Repositories
{
    public class CommentRepository: GenericRepository<Comment>
    {
        private readonly FictionDbContext _context;
        public CommentRepository(FictionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Comment>> GetCommentsByChapterIdAsync(int id)
        {
            return await  _context.Comments.Include(x => x.Author)
            .Where(x => x.ChapterId == id).ToListAsync();
        }
    }
}