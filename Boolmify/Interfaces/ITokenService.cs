    using Boolmify.Models;

    namespace Boolmify.Interfaces;

    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }