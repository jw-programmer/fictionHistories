using System.ComponentModel.DataAnnotations;

namespace Src.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string CommentText { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }   
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}