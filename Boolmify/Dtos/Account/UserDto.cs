    namespace Boolmify.Dtos.Account;

    public class UserDto
    {
        public int Id { get; set; }              // شناسه کاربر
        public string Identifier { get; set; } = default!; // ایمیل یا موبایل
        public string Role { get; set; } = "User"; // نقش کاربر (Customer, Admin)
        public bool IsBlocked { get; set; } = false; // وضعیت بلاک بودن
        public DateTime CreatedAt { get; set; }      // تاریخ عضویت
    }