    using Boolmify.Models;

    namespace Boolmify.Interfaces;

    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }