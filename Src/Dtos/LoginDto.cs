using System.ComponentModel.DataAnnotations;

namespace Src.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User name is required to login")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required to login")]
        public string Password { get; set; }
        
        
        
        
    }
}