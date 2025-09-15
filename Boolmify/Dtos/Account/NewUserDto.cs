    namespace Boolmify.Dtos.Account;

    public class NewUserDto
    {
        public string Identifier { get; set; } = default!;

        public string  Role { get; set; } = "User";
        public string Token { get; set; }  = default!;

    }