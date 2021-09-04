using System;
using System.Collections.Generic;
using Src.Enuns;
using Src.Models;

namespace Src.Dtos
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public Ranting AgeRanting { get; set; }
        public bool Finish { get; set; }
        public string AuthorId { get; set; }
        public AuthorDto Author { get; set; }
        public IList<Genre> Genres { get; set; }
    }
}