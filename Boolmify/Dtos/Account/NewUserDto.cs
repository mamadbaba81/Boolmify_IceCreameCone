    namespace Boolmify.Dtos.Account;

    public class NewUserDto
    {
        public string Identifier { get; set; } = default!;

        public string  Role { get; set; } = "User";

        public string  AccessToken { get; set; } = string.Empty;
        
        public string  RefreshToken { get; set; } = string.Empty;

    }