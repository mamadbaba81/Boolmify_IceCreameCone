    using Boolmify.Data;
    using Boolmify.Dtos.Account;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class UserRepository:IUserService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly UserManager<AppUser>  _UserManager;

        public UserRepository(ApplicationDBContext context , UserManager<AppUser> userManager)
        {
            _Context =  context;
            _UserManager = userManager;
        }
        public async Task<UserDto?> GetProfileAsync(int id)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u=>u.Id == id);
            if (user==null) return null;

            return new UserDto
            {
                Id = user.Id,
                Identifier = user.Email!,
                CreatedAt = user.CreatedAt,
                IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now,
                Role = "User"
            };
            

        }

        public async Task<UserDto?> UpdateProfileAsync(int id, UpdateUserDto dto)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;
            if (!string.IsNullOrWhiteSpace(dto.Identifier)) user.Identifier = dto.Identifier;
            
           await _Context.SaveChangesAsync();
           return new UserDto
           {
               Id = user.Id,
               Identifier = user.Email!,
               CreatedAt = user.CreatedAt,
               IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now,
               Role = "User"
           };


        }

        public async Task<bool> ChangePasswordAsync(int id, ChangePassDto dto)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            if (dto.NewPassword != dto.ConfirmPassword) throw new Exception("Passwords do not match");
            
            var resualt= await _UserManager.ChangePasswordAsync(user , dto.CurrentPassword, dto.NewPassword);
            
            return resualt.Succeeded;
        }
    }