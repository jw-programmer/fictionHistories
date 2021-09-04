using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Src.Authentication;
using Src.Dtos;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AuthorRepository _repo;

        public AuthController(IConfiguration config, AuthorRepository repo)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> LoginAsync([FromBody] LoginDto login)
        {
            var user = await _repo.GetByLogin(login);

            if(user == null)
            {
                return NotFound("Password or username is Incorrect");
            }

            var jwtToken = JwtService.GenerateJwtToken(user, _config.GetValue<string>("JWT:Secret"), await _repo.GetRolesByAuthorAsync(user));

            return Ok(new {
                user = login,
                token = jwtToken
            });
        }

    }
}