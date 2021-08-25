using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _repo;

        public CommentController(CommentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Comment>>> GetAsync([FromQuery] int id)
        {
            var comments = await _repo.GetCommentsByChapterIdAsync(id);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            return await _repo.getByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Comment comment)
        {
            if(comment == null)
            {
                return BadRequest();
            }
            await _repo.insertAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id}, comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] Comment comment)
        {
            if(comment == null || id != comment.Id){
                return BadRequest();
            }
            await _repo.updateAsync(comment);
            return NoContent();
        }
    }
}