    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Order;

    public class UpdateOrderDto
    {
        [Required]
        public int OrderId { get; set; }

        // اطلاعات گیرنده (اختیاری - فقط اگر کاربر بخواد تغییر بده)
        public string? RecipientName { get; set; }
        public string? RecipientPhone { get; set; }
        public string? RecipientAddress { get; set; }

        public DateTime? DeliveryDate { get; set; }

        // فقط ادمین می‌تونه اینو تغییر بده
        public string? Status { get; set; }
    }