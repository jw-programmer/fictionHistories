using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IGenericRepository<History> _repo;

        public HistoryController(IGenericRepository<History> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<History>>> GetAsync()
        {
            var HistoryList = await _repo.getAllAsync();
            return Ok(HistoryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetById(int id)
        {
            return await _repo.getByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] History History)
        {
            if (History == null)
            {
                return BadRequest();
            }

            await _repo.insertAsync(History);

            return CreatedAtAction(nameof(GetById), new { id = History.Id }, History);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] History History)
        {
            if (History == null || History.Id != id)
            {
                return BadRequest();
            }

            await _repo.updateAsync(History);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromBody] History History)
        {
            if (History == null || History.Id != id)
            {
                return BadRequest();
            }

            await _repo.deleteAsync(History);

            return NoContent();
        }

    }

}
