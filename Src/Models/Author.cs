using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Src.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }
        public IList<History> Histories { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}