using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Src.Authentication;
using Src.Data;
using Src.Dtos;
using Src.Models;

namespace Src.Repositories
{
    public class AuthorRepository: GenericRepository<Author>
    {
         private readonly FictionDbContext _context;
         private readonly IMapper _mapper;
         private readonly UserManager<Author> _userManager;
        public AuthorRepository(FictionDbContext context, IMapper mapper, UserManager<Author> userManager) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<AuthorDto> InsertAsync(NewAuthorDto obj)
        {
            var author = _mapper.Map<Author>(obj);
            var userExite = await _userManager.FindByNameAsync(author.UserName);
            if(userExite != null)
            {
                throw new System.Exception("User just exist");
            }
            var result = await _userManager.CreateAsync(author, obj.Password);
            if(result.Succeeded)
            {
               await _userManager.AddToRoleAsync(author, AuthorRoles.Author);
               return _mapper.Map<AuthorDto>(author);
            }
            return null;
        }
    }
}