    using Boolmify.Dtos.RefreshToken;
    using Boolmify.Models;

    namespace Boolmify.Interfaces;

    public interface ITokenService
    {
        Task<(string AccessToken , string RefreshToken)> CreateToken(AppUser user);
        
    }