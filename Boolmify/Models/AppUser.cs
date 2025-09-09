    using Microsoft.AspNetCore.Identity;

    namespace Boolmify.Models;

    public class AppUser:IdentityUser
    {
        
        public bool IsAdmin { get; set; } = false;

        public List<Review> Reviews { get; set; } = new();

        public List<Order> Orders { get; set; } = new();
        
        public List<CouponRedemption> CouponRedemptions { get; set; } = new();
    }