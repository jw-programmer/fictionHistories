using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Src.Extentions;
using Src.Models;
using Src.Queries;
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
        public async Task<ActionResult<IList<Author>>> GetAsync([FromQuery] PaginationQuery paginationQuery)
        {
            var AuthorQuery = _repo.GetAll(paginationQuery);
            if(paginationQuery != null)
            {
                await HttpContext.InsertPageMetadata<Author>(AuthorQuery, paginationQuery);
            }
            var AuthorList = await AuthorQuery.AsNoTracking().ToListAsync();
            return Ok(AuthorList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);        
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            if(author == null)
            {
                return BadRequest();
            }

            await _repo.InsertAsync(author);

            return CreatedAtAction(nameof(GetById), new {id = author.Id}, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody] Author author)
        {
            if(author == null || author.Id != id)
            {
                return BadRequest();
            }

            await _repo.UpdateAsync(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id, [FromBody] Author author)
        {
            if(author == null || author.Id != id)
            {
                return BadRequest();
            }

            await _repo.DeleteAsync(author);

            return NoContent();
        }

    }
}