namespace Src.Dtos
{
    public class CommentDto
    {
         public int Id { get; set; }
        public string CommentText { get; set; }
        public string AuthorId { get; set; }
        public string AuthorUserName { get; set; }
    }
}