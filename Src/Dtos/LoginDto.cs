using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Src.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User name is required to login")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required to login")]
        [JsonIgnore]
        public string Password { get; set; }
        
        
        
        
    }
}