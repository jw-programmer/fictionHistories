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
        public async Task<ActionResult<Author>> GetById(int id)
        {
            return await _repo.getByIdAsync(id);        
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            if(author == null)
            {
                return BadRequest();
            }

            await _repo.insertAsync(author);

            return CreatedAtAction(nameof(GetById), new {id = author.Id}, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody] Author author)
        {
            if(author == null || author.Id != id)
            {
                return BadRequest();
            }

            await _repo.updateAsync(author);

            return NoContent();
        }

    }
}