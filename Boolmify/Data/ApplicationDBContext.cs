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
            //product -- Category(1-n)
            builder.Entity<Product>().HasOne(p=>p.Category).WithMany(p=>p.Product)
                .HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.Restrict); // ta hazf daste bandi ba eth hazf hame mahsolat nashe
            
            //Produc--Comment (1-n)
            builder.Entity<Product>().HasMany(p=>p.Comments).WithOne(p=>p.Product)
                .HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade);//bepors chera
            
            //Product--Review(1-n)
            builder.Entity<Product>().HasMany(p=>p.Reviews).WithOne(p=>p.Product)
                .HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade);
            
            //Product--Occasion(n-n)
            builder.Entity<ProductOccasion>().HasKey(po => new { po.ProductId, po.OccasionId });

            builder.Entity<ProductOccasion>().HasOne(po => po.Product).WithMany(po => po.ProductOccasions)
                .HasForeignKey(po => po.ProductId);
            
            builder.Entity<ProductOccasion>().HasOne(po=>po.Occasion).WithMany(po=>po.ProductOccasions)
                .HasForeignKey(po=>po.OccasionId);
            
            //product--AddOn(n-n)
            
            builder.Entity<ProductAddOnMap>().HasKey(pa=> new {pa.ProductAddOnId , pa.ProductId});

            builder.Entity<ProductAddOnMap>().HasOne(pam => pam.Product).WithMany(pam => pam.AddOns)
                .HasForeignKey(pam => pam.ProductId);
            
            builder.Entity<ProductAddOnMap>().HasOne(pam=>pam.ProductAddOn).WithMany(pam=>pam.ProductAddOnMaps)
                .HasForeignKey(pam=>pam.ProductAddOnId);
            
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