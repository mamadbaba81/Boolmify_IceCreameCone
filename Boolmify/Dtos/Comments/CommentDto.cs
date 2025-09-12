    namespace Boolmify.Dtos.CommentsDtos;

    public class CommentDto
    {
        public int  CommentId { get; set; }

        public int  ProductId { get; set; }

        public string  UserName { get; set; } = default!;
        
        public string Content { get; set; } = default!;
        
        public DateTime CreatedAt { get; set; } 

        public int?  ParentCommentId { get; set; } 

        public List<CommentDto> Replies { get; set; } = new();
        
    }