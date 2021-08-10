using System.Collections.Generic;
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
            var genreList = await _repository.getAllAsync(); 
            return Ok(genreList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetById(int id)
        {
            return await _repository.getByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Genre genre)
        {
            if(genre == null)
            {
                return BadRequest();
            }

            await _repository.insertAsync(genre);

            return CreatedAtAction(nameof(GetById), new {id = genre.Id}, genre);
        }

    }
}