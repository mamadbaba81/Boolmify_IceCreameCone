    using Boolmify.Dtos.Account;

    namespace Boolmify.Interfaces.USerService;

    public interface IUserService
    {
        Task<UserDto?> GetProfileAsync(int id);
        
        Task<UserDto?> UpdateProfileAsync(int id , UpdateUserDto dto);
        
        Task<bool> ChangePasswordAsync(int id, ChangePassDto dto);
     }