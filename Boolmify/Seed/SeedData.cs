    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;

    namespace Boolmify;

    public class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            // نقش‌ها
            string[] roleNames = { "User", "Admin" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            // گرفتن مقادیر از secrets
            var adminEmail = config["Admin:Email"];
            var adminPassword = config["Admin:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
                throw new Exception("Admin credentials not configured in secrets!");

            // یوزر ادمین اولیه
            var adminUser = await userManager.FindByNameAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Identifier = adminEmail,
                    Role = UserRole.Admin
                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }