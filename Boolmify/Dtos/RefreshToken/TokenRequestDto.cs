    namespace Boolmify.Dtos.RefreshToken;

    public class TokenRequestDto
    {
        public string  AccessToken { get; set; } = string.Empty;
        
        public string  RefreshToken { get; set; } = string.Empty;
    }