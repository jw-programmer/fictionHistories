namespace Src.Dtos
{
    public class CommentDto
    {
         public int Id { get; set; }
        public string CommentText { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }
    }
}