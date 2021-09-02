using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Src.Enuns;
using System.Text.Json.Serialization;

namespace Src.Models
{
    public class History
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public Ranting AgeRanting { get; set; }
        public bool Finish { get; set; }
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual IList<Genre> Genres { get; set; }

        [JsonIgnore]
        public virtual IList<Chapter> Chapters { get; set; }
    }
}