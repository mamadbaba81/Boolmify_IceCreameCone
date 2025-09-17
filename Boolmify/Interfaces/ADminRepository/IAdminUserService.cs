    using Boolmify.Dtos.Account;
    using Boolmify.Helper;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminUserService
    {
        Task<QueryObject<UserDto>> GetAllAsync(string? search = null,
            int pageNumber = 1,
            int pageSize = 10);
        
        Task<UserDto?> GetByIdAsync(int id);
        
        Task<UserDto> CreateAsynce(CreateUserDto userDto);
        
        Task<UserDto> UpdateAsynce(int id, UpdateUserDto userDto);
        
        Task<bool> DeleteAsynce(int id);
        
        Task<bool> ChangeRoleAsync(int UserId , string newRole);
        
        Task<bool> ToggleBlockAsync(int UserId, bool isBlock);
        
        Task<bool> ChangePasswordAsync(int UserId, string newPassword);
    }