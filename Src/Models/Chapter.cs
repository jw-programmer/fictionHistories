using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Src.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int Index { get; set; }

        [Required]
        public string Title { get; set; }
        public int HistoryId { get; set; }
        public History History { get; set; }
        public IList<Comment> Comments { get; set; }
        
    }
}