using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Src.Authentication;
using Src.Dtos;
using Src.Models;
using Src.Repositories;

namespace Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AuthorRepository _repo;
        private readonly UserManager<Author> _userManager;

        public AuthController(IConfiguration config, AuthorRepository repo, UserManager<Author> userManager)
        {
            _config = config;
            _repo = repo;
            _userManager = userManager;
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
                token = jwtToken
            });
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<ActionResult<dynamic>> RefreshAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var jwtRefreshToken =  JwtService.GenerateJwtToken(user, _config.GetValue<string>("JWT:Secret"), await _repo.GetRolesByAuthorAsync(user));

            return Ok(new {
                token = jwtRefreshToken
            });
        }

    }
}