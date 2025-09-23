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
            //seed data
            builder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    UserId = 1,
                    RecipientName = "علی محمدی",
                    RecipientPhone = "09120000001",
                    RecipientAddress = "تهران، خیابان آزادی، پلاک 101",
                    DeliveryDate = DateTime.Now.AddDays(2),
                    Status = OrderStatus.Pending,
                    TotalAmount = 500000,
                    FinalAmouont = 450000,
                    DiscountAmount = 50000,
                    CreateAt = DateTime.Now
                },
                new Order
                {
                    OrderId = 2,
                    UserId = 2,
                    RecipientName = "زهرا رضایی",
                    RecipientPhone = "09120000002",
                    RecipientAddress = "اصفهان، میدان نقش جهان، کوچه 12",
                    DeliveryDate = DateTime.Now.AddDays(3),
                    Status = OrderStatus.Paid,
                    TotalAmount = 750000,
                    FinalAmouont = 700000,
                    DiscountAmount = 50000,
                    CreateAt = DateTime.Now
                },
                new Order
                {
                    OrderId = 3,
                    UserId = 3,
                    RecipientName = "محمد احمدی",
                    RecipientPhone = "09120000003",
                    RecipientAddress = "مشهد، خیابان امام رضا، پلاک 45",
                    DeliveryDate = DateTime.Now.AddDays(5),
                    Status = OrderStatus.Shipped,
                    TotalAmount = 1200000,
                    FinalAmouont = 1200000,
                    DiscountAmount = 0,
                    CreateAt = DateTime.Now
                },
                new Order
                {
                    OrderId = 4,
                    UserId = 4,
                    RecipientName = "سارا کیانی",
                    RecipientPhone = "09120000004",
                    RecipientAddress = "شیراز، خیابان زند، کوچه 3",
                    DeliveryDate = DateTime.Now.AddDays(1),
                    Status = OrderStatus.Delivered,
                    TotalAmount = 300000,
                    FinalAmouont = 270000,
                    DiscountAmount = 30000,
                    CreateAt = DateTime.Now
                }
            );
///

            builder.Entity<AppUser>().Property(a => a.Identifier).IsRequired();
            builder.Entity<AppUser>().HasIndex(u=>u.Identifier).IsUnique();
            //FAQ
            builder.Entity<FAQ>().Property(a => a.IsActive).HasDefaultValue(true);
            //product -- Category(1-n)
            builder.Entity<Product>().HasOne(p=>p.Category).WithMany(p=>p.Products)
                .HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.Restrict); // ta hazf daste bandi ba eth hazf hame mahsolat nashe
            
            //Produc--Comment (1-n)
            builder.Entity<Product>().HasMany(p=>p.Comments).WithOne(p=>p.Product)
                .HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade);//product hazf shod commentasham hazf she
            
            
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
            //Category--Parent/Childeren (self-refrence)
            builder.Entity<Category>().HasOne(c=>c.Parent).WithMany(c=>c.Children)
                .HasForeignKey(c=>c.ParentId).OnDelete(DeleteBehavior.Restrict);
            //Category--Product(1-n)
            builder.Entity<Category>().HasMany(c => c.Products).WithOne(c => c.Category
            ).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);
            //Comment--parent/Replies (self refrence)
            builder.Entity<Comment>().HasOne(c => c.ParentComment).WithMany(c => c.Replies).HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
            //Coment--User(n-1)
            builder.Entity<Comment>().HasOne(c=>c.User).WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.UserId).OnDelete(DeleteBehavior.Cascade);//cascade((bray inke karbr hazf shod commentasham hazf she
            
            //Comment--Product(n-1)
            builder.Entity<Comment>().HasOne(c=>c.Product).WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.ProductId).OnDelete(DeleteBehavior.Cascade); //cascade((bray inke mahsol hazf shod commentasham hazf she
            
            //Review--Product
            builder.Entity<Review>().HasOne(R=>R.Product).WithMany(r=>r.Reviews)
                .HasForeignKey(R=>R.ProductId).OnDelete(DeleteBehavior.Cascade);
            
            //Review--User
            builder.Entity<Review>().HasOne(r=>r.User).WithMany(r=>r.Reviews)
                .HasForeignKey(r=>r.UserId).OnDelete(DeleteBehavior.Cascade);
            
            //Cart--User(n-1)
            builder.Entity<Cart>().HasOne(c=>c.User).WithMany().HasForeignKey(c=>c.USerId).OnDelete(DeleteBehavior.Cascade);
            //Cart--CartItem (1-n)
            builder.Entity<Cart>().HasMany(c=>c.Items).WithOne(c=>c.Cart).HasForeignKey(c=>c.CartId).OnDelete(DeleteBehavior.Cascade);
            
            //CartItem--Product(1-n)
            builder.Entity<CartItem>().HasOne(ci=>ci.Product).WithMany().HasForeignKey(ci=>ci.ProductId).OnDelete(DeleteBehavior.Restrict);
            
            //CartItemAddOn--ProductAddOn(n-n)
            builder.Entity<CartItemAddOn>().HasKey(ci=>new {ci.CartItemId,ci.ProductAddOnId});
            
            builder.Entity<CartItemAddOn>().HasOne(cia=>cia.CartItem).WithMany(ci=>ci.CartItemAddOns).HasForeignKey(ci=>ci.CartItemId);
            builder.Entity<CartItemAddOn>().HasOne(cia=>cia.ProductAddOn).WithMany().HasForeignKey(ci=>ci.ProductAddOnId);
            //Order--User(n-1)
            builder.Entity<Order>().HasOne(o=>o.User).WithMany(o=>o.Orders)
                .HasForeignKey(o=>o.UserId).OnDelete(DeleteBehavior.Cascade);
            
            //Order--OrderItem(1-n)
            builder.Entity<Order>().HasMany(o=>o.OrderItems).WithOne(o=>o.Order)
                .HasForeignKey(o=>o.OrderId).OnDelete(DeleteBehavior.Cascade);
            //Order--Delivery(1-1)
            builder.Entity<Order>().HasOne(o=>o.Delivery).WithOne(d=>d.Order).HasForeignKey<Delivery>
                (o=>o.OrderId).OnDelete(DeleteBehavior.Cascade);//<Delivery > ro bpors
            //Order--Payment (1-n)
            builder.Entity<Order>().HasMany(o=>o.Payments).WithOne(o=>o.Order)
                .HasForeignKey(o=>o.OrderId).OnDelete(DeleteBehavior.Cascade);
            
            //OrderItem--product(n-1)
            builder.Entity<OrderItem>().HasOne(oi=>oi.Product).WithMany()
                .HasForeignKey(oi=>oi.ProductId).OnDelete(DeleteBehavior.Restrict);
            
            //OrderItem--ProductAddOn(n-n)
            builder.Entity<OrderItemAddOn>().HasKey(oi => new {oi.OrderItemId,oi.ProductAddOnId});
            
            builder.Entity<OrderItemAddOn>().HasOne(oia=>oia.OrderItem).WithMany(oia=>oia.OrderItemsAddOns).HasForeignKey(oia=>oia.OrderItemId);
            builder.Entity<OrderItemAddOn>().HasOne(oia=>oia.ProductAddOn).WithMany().HasForeignKey(oia=>oia.ProductAddOnId);
            
            //Payment--Order(1-n)
            builder.Entity<Payment>().HasOne(p=>p.Order).WithMany(o=>o.Payments)
                .HasForeignKey(p=>p.OrderId).OnDelete(DeleteBehavior.Cascade);
            //couponredemption composite Key
            builder.Entity<CouponRedemption>()
                .HasKey(cr => new { cr.CouponId, cr.UserId, cr.OrderId });

            //Coupon--CouponRedemption(1-n)
            builder.Entity<Coupon>().HasMany(c=>c.CouponRedemptions).WithOne(c=>c.Coupon)
                .HasForeignKey(c=>c.CouponId).OnDelete(DeleteBehavior.Cascade);
            
            //CouponRedemption--User(n-1)
            builder.Entity<CouponRedemption>().HasOne(cr=>cr.user).WithMany().HasForeignKey(cr=>cr.UserId).OnDelete(DeleteBehavior.Restrict);
            
            //CouponRedemption--Order(n-1)
            
            builder.Entity<CouponRedemption>().HasOne(cr=>cr.Order).WithMany().HasForeignKey(cr=>cr.OrderId).OnDelete(DeleteBehavior.Cascade);
            
            //Ticket--User(n-1)
            
            builder.Entity<Ticket>().HasOne(t=>t.User).WithMany(t=>t.Tickets).HasForeignKey(t=>t.UserId).OnDelete(DeleteBehavior.Restrict);
            
            //Ticket--Order(n-1)
            builder.Entity<Ticket>().HasOne(t=>t.Order).WithMany(t=>t.Tickets).HasForeignKey(t=>t.OrderId).OnDelete(DeleteBehavior.Cascade);
            
            //Ticket--TicketMessage(1-n)
            builder.Entity<TicketMessage>().HasOne(tm=>tm.Ticket).WithMany(tm=>tm.Messages)
                .HasForeignKey(tm=>tm.TicketId).OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<TicketMessage>().HasOne(tm=>tm.Sender).WithMany()
                .HasForeignKey(tm=>tm.SenderId).OnDelete(DeleteBehavior.Restrict);
            
            //Delivery--Order(1-1)
            builder.Entity<Delivery>().HasOne(d=>d.Order).WithOne(d=>d.Delivery)
                .HasForeignKey<Delivery>(d=>d.OrderId).OnDelete(DeleteBehavior.Cascade);
            
            
            
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
        public DbSet<Banner> Banners { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Delivery> Deliveries { get; set; } = default!;
        public DbSet<Occasion> Occasions { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<FAQ> FAQs { get; set; } = default!;
        public DbSet<TicketMessage> TicketMessages { get; set; } = default!;
    }