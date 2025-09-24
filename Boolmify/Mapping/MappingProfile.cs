    using AutoMapper;
    using Boolmify.Dtos.Account;
    using Boolmify.Dtos.BannerDto;
    using Boolmify.Dtos.Cart;
    using Boolmify.Dtos.Category;
    using Boolmify.Dtos.Coupon;
    using Boolmify.Dtos.Delivery;
    using Boolmify.Dtos.FAQ;
    using Boolmify.Dtos.Occasion;
    using Boolmify.Dtos.Order;
    using Boolmify.Dtos.Payment;
    using Boolmify.Dtos.Product;
    using Boolmify.Dtos.Review;
    using Boolmify.Dtos.Ticket;
    using Boolmify.Models;

    namespace Boolmify.Mapping;

    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
              // Account / User
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, AppUser>();
            CreateMap<UpdateUserDto, AppUser>();

            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Cart
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));
            CreateMap<AddToCartDto, CartItem>();
            CreateMap<UpdateCartItemDto, CartItem>();

            // Order
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.AddOns, opt => opt.MapFrom(src => src.OrderItemsAddOns));
            CreateMap<OrderItemAddOn, OrderItemAddOnDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<UpdateOrderDto, Order>();

            // Coupon
            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>();

            // Review
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            // Review → ReviewDto
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

// CreateReviewDto → Review
            CreateMap<CreateReviewDto, Review>();

// UpdateReviewDto → Review
            CreateMap<UpdateReviewDto, Review>();

            // Ticket
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<TicketMessage, TicketMessageDto>().ReverseMap();
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<CreateTicketMessageDto, TicketMessage>();
            CreateMap<UpdateTicketStatusDto, Ticket>();

            // Banner
            CreateMap<Banner, BannerDto>().ReverseMap();
            CreateMap<CreateBannerDto, Banner>();
            CreateMap<UpdateBannerDto, Banner>();

            // Delivery
            CreateMap<Delivery, DeliveryDto>().ReverseMap();
            CreateMap<AssignDeliveryDto, Delivery>();

            // FAQ
            CreateMap<FAQ, FAQDto>().ReverseMap();
            CreateMap<CreateFAQDto, FAQ>();
            CreateMap<UpdateFAQDto, FAQ>();

            // Occasion
            CreateMap<Occasion, OccasionDto>().ReverseMap();
            CreateMap<CreateOccasionDto, Occasion>();
            CreateMap<UpdateOccasionDto, Occasion>();

            // Payment
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<CraetePaymentDto, Payment>();
            CreateMap<UpdatePaymentStatusDto, Payment>();
        }
    }