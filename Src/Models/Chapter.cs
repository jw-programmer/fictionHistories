using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Src.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int Index { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int HistoryId { get; set; }
        [JsonIgnore]
        public virtual History History { get; set; }
        [JsonIgnore]
        public virtual IList<Comment> Comments { get; set; }

    }
}