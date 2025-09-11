    namespace Boolmify.Dtos.CommentsDtos;

    public class CreateCommentDto
    {
        public int  ProductId { get; set; }
        
        public string  Content { get; set; }
        
        public int  ParentCommentId { get; set; }
        
    }