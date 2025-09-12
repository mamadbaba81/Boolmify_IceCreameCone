    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Data;

    public class ApplicationDBContext:IdentityDbContext<AppUser , IdentityRole<int>,int>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> Options) : base(Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<CartItemAddOn>()
                .HasKey(ca => new { ca.CartItemId, ca.ProductAddOnId });

            builder.Entity<OrderItemAddOn>()
                .HasKey(oa => new { oa.OrderItemId, oa.ProductAddOnId });

            builder.Entity<ProductOccasion>()
                .HasKey(po => new { po.ProductId, po.OccasionId });

            builder.Entity<ProductAddOnMap>() // مطمئن شو همین اسم توی کلاسته
                .HasKey(pm => new { pm.ProductId, pm.ProductAddOnId });
            
            builder.Entity<CouponRedemption>().HasKey(c=>new {c.CouponId ,c.UserId , c.OrderId}  );
        }

        // Cart
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<CartItemAddOn> CartItemAddOns { get; set; } = default!;

        // Order
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<OrderItemAddOn> OrderItemAddOns { get; set; } = default!;

        // Product
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductAddOn> ProductAddOns { get; set; } = default!;
        public DbSet<ProductAddOnMap> ProductAddOnMaps { get; set; } = default!;
        public DbSet<ProductOccasion> ProductOccasions { get; set; } = default!;

        // Coupon
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DbSet<CouponRedemption> CouponRedemptions { get; set; } = default!;

        // Other
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Delivery> Deliveries { get; set; } = default!;
        public DbSet<Courier> Couriers { get; set; } = default!;
        public DbSet<Occasion> Occasions { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<FAQ> FAQs { get; set; } = default!;
    }