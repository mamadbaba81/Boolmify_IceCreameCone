    using Boolmify.Data;
    using Boolmify.Dtos.Account;
    using Boolmify.Helper;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
   


    namespace Boolmify.Repository.AdminRepository;

    public class AdminUserRepository : IAdminUserService
    {

        private readonly ApplicationDBContext _Context;
        private readonly UserManager<AppUser> _UserManager;

        public AdminUserRepository(ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _Context = context;
            _UserManager = userManager;
        }

        public async Task<QueryObject<UserDto>> GetAllAsync(string? search = null, int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _Context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var Lowered = search.ToLower();

                query = query.Where(q => q.Identifier.ToLower().Contains(Lowered));

            }

            var totalCount = await query.CountAsync();
            var users = await query.OrderByDescending(u => u.CreatedAt)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = new List<UserDto>();
            foreach (var u in users)
            {
                var roles = await _UserManager.GetRolesAsync(u);
                result.Add(new UserDto
                {
                    Id = u.Id,
                    Identifier = u.Identifier,
                    Role = roles.FirstOrDefault() ?? "User",
                    IsBlocked = u.LockoutEnd != null && u.LockoutEnd > DateTimeOffset.Now,
                    CreatedAt = u.CreatedAt
                });
            }

            return new QueryObject<UserDto>
            {
                Items = result,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _Context.Users.FindAsync(id);
            if (user == null) return null;
            var roles = await _UserManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                Identifier = user.Identifier,
                Role = roles.FirstOrDefault() ?? "User",
                IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto> CreateAsynce(CreateUserDto userDto)
        {
            var user = new AppUser
            {
                UserName = userDto.Identifier,
                Identifier = userDto.Identifier,
                Email = userDto.Identifier,
                CreatedAt = DateTime.Now
            };
            var result = await _UserManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                var erors = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new Exception($"ijad karbar na movagh bood{erors}");
            }

            if (!string.IsNullOrWhiteSpace(userDto.Role))
            {
                await _UserManager.AddToRoleAsync(user, userDto.Role);
            }

            return new UserDto
            {
                Id = user.Id,
                Identifier = user.Identifier,
                Role = userDto.Role,
                IsBlocked = false,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto> UpdateAsynce(int id, UpdateUserDto userDto)
        {
            var user = await _Context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");
            user.Identifier = userDto.Identifier;
            user.Email = userDto.Identifier;
            user.UserName = userDto.Identifier;
            if (userDto.IsBlocked.HasValue)
            {
                if (userDto.IsBlocked.Value) user.LockoutEnd = DateTimeOffset.MaxValue;
                else user.LockoutEnd = null;


            }

            var result = await _UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($" Update anjam nashod : {errors}");

            }

            if (!string.IsNullOrWhiteSpace(userDto.Role))
            {
                var currentRole = await _UserManager.GetRolesAsync(user);
                if (currentRole.Any())
                    await _UserManager.RemoveFromRolesAsync(user, currentRole);

                await _UserManager.AddToRoleAsync(user, userDto.Role);
            }

            return new UserDto
                {
                    Id = user.Id,
                    Identifier = user.Identifier,
                    Role = userDto.Role ?? "User",
                    IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now,
                    CreatedAt = user.CreatedAt
                };
            }
        

        public async Task<bool> DeleteAsynce(int id)
            {
              var user = await _Context.Users.FindAsync(id);
              if (user == null) throw new Exception("User not found");
            var userresult=  await _UserManager.DeleteAsync(user);
              return userresult.Succeeded;
              
              
            }

            public async Task<bool> ChangeRoleAsync(int UserId, string newRole)
            {
               var user = await _Context.Users.FindAsync(UserId);
               if (user == null) throw new Exception("User not found");
               var roles = await _UserManager.GetRolesAsync(user);
               if (!roles.Any())
               {
                   var removeRole = await _UserManager.RemoveFromRolesAsync(user, roles);
                   if (!removeRole.Succeeded)
                   {
                       var errors = string.Join(", ", removeRole.Errors.Select(x => x.Description));
                       throw new Exception($" khata dar hazf naghsh{errors}");
                   }
               }
               var addRole = await _UserManager.AddToRoleAsync(user, newRole);
               if (!addRole.Succeeded)
               {
                   var errors = string.Join(", ", addRole.Errors.Select(x => x.Description));
                   throw new Exception($" khata dar addkardan naghsh{errors}");
               }
               return true;
            }

            public async Task<bool> ToggleBlockAsync(int UserId, bool isBlock)
            {
             var  user = await _Context.Users.FindAsync(UserId);
             if (user == null) throw new Exception("User not found");
             if (isBlock)
             {
                 user.LockoutEnd = DateTimeOffset.MaxValue;
             }
             else
             {
                 user.LockoutEnd = null;
             }
             var result = await _UserManager.UpdateAsync(user);
             if (!result.Succeeded)
             {
                 var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                 throw new Exception($" khata dar Taghyir  vazeiat{errors}");
                 
             }
             return true;
            }

            public async Task<bool> ChangePasswordAsync(int UserId, string newPassword)
            {
               var user = await _Context.Users.FindAsync(UserId);
               if (user == null) throw new Exception("User not found");
               var removepassword = await _UserManager.RemovePasswordAsync(user);
               if (!removepassword.Succeeded)
               {
                   var  errors = string.Join(", ", removepassword.Errors.Select(x => x.Description));
                   throw new Exception($" khata dar hazf pass{errors}");
               }
               var addpassword = await _UserManager.AddPasswordAsync(user, newPassword);
               if (!addpassword.Succeeded)
               {
                   var errors = string.Join(", ", addpassword.Errors.Select(x => x.Description));
                   throw new Exception($" khata dar add pass{errors}");
               }
               return true;
            } 
    }
    