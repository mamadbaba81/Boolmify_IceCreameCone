    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.CommentsDtos;

    public class UpdateCommentDto
    {
        [Required]
        public int  CommentId { get; set; }

        [Required]
        [StringLength(1000)] 
        public string Content { get; set; } = default!;
        

    }