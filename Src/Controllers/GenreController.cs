using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenericRepository<Genre> _repository;

        public GenreController(IGenericRepository<Genre> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Genre>>> Get()
        {
            var genreList = await _repository.GetAll(null).ToListAsync(); 
            return Ok(genreList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Genre genre)
        {
            if(genre == null)
            {
                return BadRequest();
            }

            await _repository.InsertAsync(genre);

            return CreatedAtAction(nameof(GetById), new {id = genre.Id}, genre);
        }

    }
}