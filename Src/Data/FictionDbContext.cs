using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Src.Models;

namespace Src.Data
{
    public class FictionDbContext: IdentityDbContext<Author>
    {
        public FictionDbContext(DbContextOptions<FictionDbContext>  options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        } 

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}