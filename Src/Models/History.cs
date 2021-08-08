using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Src.Enuns;

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
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Chapter> Chapters { get; set; }
    }
}