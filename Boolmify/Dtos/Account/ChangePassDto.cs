    namespace Boolmify.Dtos.Account;

    public class ChangePassDto
    {
        public string  CurrentPassword  { get; set; } =default!;
        public string  NewPassword  { get; set; } =default!;
        public string  ConfirmPassword { get; set; } =default!;
    }