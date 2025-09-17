    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Account;

    public class CreateUserDto
    {
            [Required]
            public string Identifier { get; set; } = default!;

            [Required]
            public string Password { get; set; } = default!;

            public string Role { get; set; } = "User";
    }