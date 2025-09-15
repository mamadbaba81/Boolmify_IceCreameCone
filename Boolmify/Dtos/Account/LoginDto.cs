    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Account;

    public class LoginDto
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Password { get; set; }

    }