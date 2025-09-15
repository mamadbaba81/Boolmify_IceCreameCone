    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.CommentsDtos;

    public class CreateCommentDto
    {
        [Required]
        public int  ProductId { get; set; }
        [Required]
        [StringLength(1000)]
        public string  Content { get; set; } =  default!;
        
        public int?  ParentCommentId { get; set; }
        
    }