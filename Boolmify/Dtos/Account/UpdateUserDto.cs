    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Account;

    public class UpdateUserDto
    {
        [Required]
        public string Identifier { get; set; } = default!;

        public string Role { get; set; } = "User";
        
        public bool? IsBlocked { get; set; } = false;
    }