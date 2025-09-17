    using Boolmify.Dtos.Account;
    using Boolmify.Helper;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminUserService
    {
        Task<QueryObject<UserDto>> GetAllAsync(string? search = null,
            int pageNumber = 1,
            int pageSize = 10);
        
        Task<UserDto?> GetByIdAsync(int id);
        
        Task<UserDto> CreateAsync(CreateUserDto userDto);
        
        Task<UserDto> UpdateAsync(int id, UpdateUserDto userDto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<bool> ChangeRoleAsync(int UserId , string newRole);
        
        Task<bool> ToggleBlockAsync(int UserId, bool isBlock);
        
        Task<bool> ChangePasswordAsync(int UserId, string newPassword);
    }