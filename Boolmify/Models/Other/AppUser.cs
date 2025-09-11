    using Microsoft.AspNetCore.Identity;

    namespace Boolmify.Models;

    public enum UserRole
    {
        Customer = 0,
        Admin = 1
    }

    public class AppUser:IdentityUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        
        public string PasswordHash { get; set; } = default!;

        public UserRole Role { get; set; } = UserRole.Customer;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ارتباطات
        public virtual List<Order> Orders { get; set; } = new();
        
        public virtual List<Comment> Comments { get; set; } = new();
        
        public virtual List<Review> Reviews { get; set; } = new();
        
    }