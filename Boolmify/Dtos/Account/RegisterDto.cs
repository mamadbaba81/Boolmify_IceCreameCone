    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Account;

    public class RegisterDto
    {
        [Required]
        public string? Identifier { get; set; } 

        [Required]
        public string? Password { get; set; } = default!;
        [Required]
        [Compare("Password",ErrorMessage = "Passwords do not match.")]
        public string ConfirmPssword { get; set; } = default!;

    }