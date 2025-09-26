    using Microsoft.AspNetCore.Identity;

    namespace Boolmify.Models;

    public enum UserRole
    {
        Customer = 0,
        Admin = 1
    }

    public class AppUser:IdentityUser<int>
    {
        
        public string Identifier { get; set; } = string.Empty;
        
        public UserRole Role { get; set; } = UserRole.Customer;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // ارتباطات
        public virtual List<Order> Orders { get; set; } = new();
        
        public virtual List<Comment> Comments { get; set; } = new();
        
        public virtual List<Review> Reviews { get; set; } = new();

        public List<RefreshToken> RefreshTokens { get; set; } = new ();

        public virtual List<Ticket> Tickets { get; set; } = new();

    }