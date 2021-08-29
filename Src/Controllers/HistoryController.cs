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
    public class HistoryController : ControllerBase
    {
        private readonly HistoryRepository _repo;

        public HistoryController(HistoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<History>>> GetAsync([FromQuery] PaginationQuery paginationQuery)
        {
            var HistoryQuery = _repo.GetAll(paginationQuery);
            if(paginationQuery != null)
            {
                await HttpContext.InsertPageMetadata<History>(HistoryQuery, paginationQuery);
            }
            var HistoryList = await HistoryQuery.AsNoTracking().ToListAsync();
            return Ok(HistoryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        [HttpGet("genre")]
        public async Task<ActionResult<IList<History>>> GetByGenre([FromQuery] Genre genre)
        {
            var HistoryList = await _repo.GetByGenre(genre);
            return Ok(HistoryList);
        }

        [HttpGet("title")]
        public async Task<ActionResult<IList<History>>> GetByTitle([FromQuery] string title)
        {
            var HistoryList = await _repo.GetByTitle(title);
            return Ok(HistoryList);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] History History)
        {
            if (History == null)
            {
                return BadRequest();
            }

            await _repo.InsertAsync(History);

            return CreatedAtAction(nameof(GetById), new { id = History.Id }, History);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] History History)
        {
            if (History == null || History.Id != id)
            {
                return BadRequest();
            }

            await _repo.UpdateAsync(History);

            return NoContent();
        }

        [HttpPut("{id}/RemoveGenre")]
        public async Task<ActionResult> PutGenres([FromRoute] int id, [FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            await _repo.UpdateGenresAsync(id, genre);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromBody] History History)
        {
            if (History == null || History.Id != id)
            {
                return BadRequest();
            }

            await _repo.DeleteAsync(History);

            return NoContent();
        }

    }

}
