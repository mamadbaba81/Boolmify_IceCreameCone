    namespace Boolmify.Models;

    public class Comment
    {
        public int  CommentId { get; set; }
        //mahsol
        public int  ProductId { get; set; }

        public virtual Product Product { get; set; } = default!;
        //karbar
        public int  UserId { get; set; }

        public virtual AppUser User { get; set; } = default!;

        //sakhtar derakhti
        public int?  ParentCommentId { get; set; }
        
        public virtual Comment ParentComment { get; set; }

        public virtual List<Comment> Replies { get; set; } = new();

        public string  Content { get; set; } = default!;


        public DateTime  CreatedAt { get; set; } =  DateTime.Now;
    }