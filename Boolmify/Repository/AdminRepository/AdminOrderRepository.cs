    using Boolmify.Data;
    using Boolmify.Dtos.Order;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminOrderRepository:IAdminOrderService
    {
        private readonly ApplicationDBContext  _Context;

        public AdminOrderRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<OrderDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? userId = null, string? status = null)
        {
            var query = _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi=>oi.Product)
                .Include(o => o.OrderItems)
                .ThenInclude(oia=>oia.OrderItemsAddOns)
                .ThenInclude(o => o.ProductAddOn)
                .Include(p=>p.Payments)
                .Include(cr=>cr.CouponRedemptions).AsQueryable();
            
            if (userId.HasValue) query = query.Where(o => o.UserId == userId);

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<OrderStatus>(status, true, out var parsedStatus)) 
                query = query.Where(o => o.Status == parsedStatus);
            return await query.OrderByDescending(o => o.CreateAt).Skip
                ((pageNumber - 1) * pageSize).Take(pageSize).Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                RecipientName = o.RecipientName,
                RecipientPhone = o.RecipientPhone,
                RecipientAddress = o.RecipientAddress,
                DeliveryDate = o.DeliveryDate,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                CreateAt = o.CreateAt,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    AddOns = oi.OrderItemsAddOns.Select(oa => new OrderItemAddOnDto
                    {
                        ProductAddOnId = oa.ProductAddOnId,
                        Name = oa.ProductAddOn.ProductAddOnsName,
                        Price = oa.ProductAddOn.Price
                    }).ToList()
                }).ToList(),
                Payments = o.Payments.Select(p => new Dtos.Payment.PaymentDto
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    Method = p.Method.ToString(),
                    Status = p.Status.ToString(),
                    PaidAt = p.PaidAt
                }).ToList(),
                Coupons = o.CouponRedemptions != null
                    ? new List<Dtos.Coupon.CouponDto>
                    {
                        new Dtos.Coupon.CouponDto
                        {
                            CouponId = o.CouponRedemptions.CouponId,
                            Code = o.CouponRedemptions.Coupon.Code,
                            DiscountType = o.CouponRedemptions.Coupon.DiscountType.ToString(),
                            Value = o.CouponRedemptions.Coupon.Value,
                            MaxDiscountAmount = o.CouponRedemptions.Coupon.MaxDiscountAmount,
                            MinOrderAmount = o.CouponRedemptions.Coupon.MinOrderAmount,
                            ValidForm = o.CouponRedemptions.Coupon.ValidFrom,
                            ValidTo = o.CouponRedemptions.Coupon.ValidTo,
                            IsActive = o.CouponRedemptions.Coupon.IsActive
                        }
                    }
                    : new List<Dtos.Coupon.CouponDto>()

            }).ToListAsync();
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        { 
            var order = await _Context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.OrderItemsAddOns).ThenInclude(oa => oa.ProductAddOn)
                .Include(o => o.Payments)
                .Include(o => o.CouponRedemptions).ThenInclude(cr => cr.Coupon)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;

            return new OrderDto
            {

                OrderId = order.OrderId,
                UserId = order.UserId,
                RecipientName = order.RecipientName,
                RecipientPhone = order.RecipientPhone,
                RecipientAddress = order.RecipientAddress,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                CreateAt = order.CreateAt,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    AddOns = oi.OrderItemsAddOns.Select(oa => new OrderItemAddOnDto
                    {
                        ProductAddOnId = oa.ProductAddOnId,
                        Name = oa.ProductAddOn.ProductAddOnsName,
                        Price = oa.ProductAddOn.Price
                    }).ToList()
                }).ToList(),
                Payments = order.Payments.Select(p => new Dtos.Payment.PaymentDto
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    Status = p.Status.ToString(),
                    PaidAt = p.PaidAt
                }).ToList(),
                Coupons = order.CouponRedemptions != null
                    ? new List<Dtos.Coupon.CouponDto>
                    {
                        new Dtos.Coupon.CouponDto
                        {
                            CouponId = order.CouponRedemptions.CouponId,
                            Code = order.CouponRedemptions.Coupon.Code,
                            DiscountType = order.CouponRedemptions.Coupon.DiscountType.ToString(),
                            Value = order.CouponRedemptions.Coupon.Value,
                            MaxDiscountAmount = order.CouponRedemptions.Coupon.MaxDiscountAmount,
                            MinOrderAmount = order.CouponRedemptions.Coupon.MinOrderAmount,
                            ValidForm = order.CouponRedemptions.Coupon.ValidFrom,
                            ValidTo = order.CouponRedemptions.Coupon.ValidTo,
                            IsActive = order.CouponRedemptions.Coupon.IsActive
                        }
                    }
                    : new List<Dtos.Coupon.CouponDto>()
            };

        }

        public async Task<OrderDto> UpdateStatusAsync(int id, string newStatus)
        {
            
            var order = await _Context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.OrderItemsAddOns).ThenInclude(oa => oa.ProductAddOn)
                .Include(o => o.Payments)
                .Include(o => o.CouponRedemptions).ThenInclude(cr => cr.Coupon)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;

           
    if (!Enum.TryParse<OrderStatus>(newStatus, true, out var parsedStatus))
        throw new Exception($"Invalid status: {newStatus}");

    order.Status = parsedStatus;
    await _Context.SaveChangesAsync();

    // همون خروجی مشابه GetByIdAsync
    return new OrderDto
    {
        OrderId = order.OrderId,
        UserId = order.UserId,
        RecipientName = order.RecipientName,
        RecipientPhone = order.RecipientPhone,
        RecipientAddress = order.RecipientAddress,
        DeliveryDate = order.DeliveryDate,
        Status = order.Status.ToString(),
        TotalAmount = order.TotalAmount,
        CreateAt = order.CreateAt,
        Items = order.OrderItems.Select(oi => new OrderItemDto
        {
            OrderItemId = oi.OrderItemId,
            ProductId = oi.ProductId,
            ProductName = oi.Product.ProductName,
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice,
            AddOns = oi.OrderItemsAddOns.Select(oa => new OrderItemAddOnDto
            {
                ProductAddOnId = oa.ProductAddOnId,
                Name = oa.ProductAddOn.ProductAddOnsName,
                Price = oa.ProductAddOn.Price
            }).ToList()
        }).ToList(),
        Payments = order.Payments.Select(p => new Dtos.Payment.PaymentDto
        {
            PaymentId = p.PaymentId,
            OrderId = p.OrderId,
            Amount = p.Amount,
            Method = p.Method.ToString(),
            Status = p.Status.ToString(),
            PaidAt = p.PaidAt
        }).ToList(),
        Coupons = order.CouponRedemptions != null
            ? new List<Dtos.Coupon.CouponDto>
            {
                new Dtos.Coupon.CouponDto
                {
                    CouponId = order.CouponRedemptions.CouponId,
                    Code = order.CouponRedemptions.Coupon.Code,
                    DiscountType = order.CouponRedemptions.Coupon.DiscountType.ToString(),
                    Value = order.CouponRedemptions.Coupon.Value,
                    MaxDiscountAmount = order.CouponRedemptions.Coupon.MaxDiscountAmount,
                    MinOrderAmount = order.CouponRedemptions.Coupon.MinOrderAmount,
                    ValidForm = order.CouponRedemptions.Coupon.ValidFrom,
                    ValidTo = order.CouponRedemptions.Coupon.ValidTo,
                    IsActive = order.CouponRedemptions.Coupon.IsActive
                }
            }
            : new List<Dtos.Coupon.CouponDto>()
    };

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _Context.Orders.Include
                (o=>o.OrderItems).FirstOrDefaultAsync(o=>o.OrderId == id);
            if (order == null) return false;
            _Context.OrderItems.RemoveRange(order.OrderItems);
            _Context.Orders.Remove(order);
            await _Context.SaveChangesAsync();
            return true;

        }
    }