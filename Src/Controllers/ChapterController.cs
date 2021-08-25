using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : ControllerBase
    {
        private readonly ChapterRepository _repo;
        public ChapterController(ChapterRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Chapter>>> GetByHistoryAsync([FromQuery] int historyId)
        {
            var chapters = await _repo.GetByHistoryIdAsync(historyId);
            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chapter>> GetById(int id)
        {
            return await _repo.getByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Chapter chapter)
        {
            if(chapter == null){
                return BadRequest();
            }
            await _repo.insertAsync(chapter);

            return CreatedAtAction(nameof(GetById), new {id = chapter.Id}, chapter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Chapter chapter)
        {
            if (chapter == null || chapter.Id != id)
            {
                return BadRequest();
            }

            await _repo.updateAsync(chapter);

            return NoContent();
        }
    }
}