using System.ComponentModel.DataAnnotations;

namespace Src.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string CommentText { get; set; }
        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }   
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}