using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IGenericRepository<Author> _repo;

        public AuthorController(IGenericRepository<Author> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Author>>> GetAsync()
        {
            var AuthorList = await _repo.getAllAsync();
            return Ok(AuthorList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAsync(int id)
        {
            return await _repo.getByIdAsync(id);        
        }
    }
}