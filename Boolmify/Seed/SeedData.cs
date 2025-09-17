    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

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

        // گرفتن اطلاعات ادمین از secrets
        var adminIdentifier = config["Admin:Identifier"];
        var adminEmail = config["Admin:Email"];
        var adminPassword = config["Admin:Password"];

        if (string.IsNullOrEmpty(adminIdentifier) || string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            throw new Exception("⚠️ Admin credentials not configured in secrets!");

        // چک بر اساس Identifier
        var adminUser = await userManager.Users.FirstOrDefaultAsync(u => u.Identifier == adminIdentifier);
        if (adminUser == null)
        {
            var newAdmin = new AppUser
            {
                UserName = adminIdentifier,
                Email = adminEmail,
                Identifier = adminIdentifier,
                EmailConfirmed = true // ایمیل رو تأییدشده می‌ذاره
            };

            var result = await userManager.CreateAsync(newAdmin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
        else
        {
            // اگر قبلا ساخته شده بود ولی Identifier پر نشده بود → آپدیتش کن
            if (string.IsNullOrEmpty(adminUser.Identifier))
            {
                adminUser.Identifier = adminIdentifier;
                await userManager.UpdateAsync(adminUser);
            }
        }
    }
}