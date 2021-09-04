using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Src.Dtos;
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
        private readonly AuthorRepository _repo;
        private readonly IMapper _mapper;

        public AuthorController(AuthorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<AuthorDto>>> GetAsync([FromQuery] PaginationQuery paginationQuery)
        {
            var AuthorQuery = _repo.GetAll(paginationQuery);
            if(paginationQuery != null)
            {
                await HttpContext.InsertPageMetadata<Author>(AuthorQuery, paginationQuery);
            }
            var AuthorList = await AuthorQuery.AsNoTracking().ProjectToListAsync<AuthorDto>();
            return Ok(AuthorList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(string id)
        {
            var author = await _repo.GetByIdAsync(id); 
            return _mapper.Map<AuthorDto>(author);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody, BindRequired] NewAuthorDto author)
        {
            if(author == null)
            {
                return BadRequest();
            }

            var obj = await _repo.InsertAsync(author);
            return CreatedAtAction(nameof(GetById), new {id = obj.Id}, obj);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]string id, [FromBody] AuthorDto authorDto)
        {
            if(authorDto == null || authorDto.Id != id)
            {
                return BadRequest();
            }
            var author = _mapper.Map<Author>(authorDto);
            await _repo.UpdateAsync(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]string id, [FromBody] AuthorDto authorDto)
        {
            if(authorDto == null || authorDto.Id != id)
            {
                return BadRequest();
            }
            var author = _mapper.Map<Author>(authorDto);
            await _repo.DeleteAsync(author);

            return NoContent();
        }

    }
}