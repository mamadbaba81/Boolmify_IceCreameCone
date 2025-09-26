    using Boolmify.Dtos.RefreshToken;
    using Boolmify.Models;

    namespace Boolmify.Interfaces;

    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        
       // Task<TokenResponseDto>  RefreshTokenAsync(TokenRequestDto dto);
    }