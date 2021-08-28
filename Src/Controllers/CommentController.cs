using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Src.Dtos;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _repo;
        private readonly IMapper _mapper;
        public CommentController(CommentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CommentDto>>> GetAsync([FromQuery] int id)
        {
            var comments = await _repo.GetCommentsByChapterIdAsync(id);
            IList<CommentDto> mapComments = new List<CommentDto>();
            foreach (var item in comments)
            {
                mapComments.Add(_mapper.Map<CommentDto>(item));
            }
            return Ok(mapComments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Comment comment)
        {
            if(comment == null)
            {
                return BadRequest();
            }
            await _repo.InsertAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id}, comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] Comment comment)
        {
            if(comment == null || id != comment.Id){
                return BadRequest();
            }
            await _repo.UpdateAsync(comment);
            return NoContent();
        }
    }
}