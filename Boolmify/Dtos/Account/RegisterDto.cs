    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Account;

    public class RegisterDto
    {
        [Required]
        public String? UserName { get; set; }
        [Required]
        [EmailAddress]
        public String? Email{ get; set; }
        [Required]
        public String? Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPssword { get; set; }

    }