using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Src.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public IList<History> Histories { get; set; }


    }
}