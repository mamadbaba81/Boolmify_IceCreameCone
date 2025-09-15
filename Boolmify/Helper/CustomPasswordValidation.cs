    using System.Text.RegularExpressions;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;

    namespace Boolmify.Helper;

    public class CustomPasswordValidation<TUser>:IPasswordValidator<TUser> where TUser : class
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            var CommenPatterns = new List<string>
            {
                "123456", "1234", "12345678", "1111", "qwerty", "asdfgh", "zxcvb", "password", "abcdefghi"
            };
            string Lowered = password.ToLower();
            if (CommenPatterns.Any(p => Lowered.Contains(p)))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError()
                {
                    Code = "WeakPass",
                    Description = "Password is too simple or predictable. Please choose a stronger one."
                }));
            
            }
        
            if (password.Distinct().Count() < 4)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError()
                {
                    Code = "LowEntropy",
                    Description = "Password contains too many repeated characters. Please use more variety."

                }));
           
            }

            if (user is AppUser appUser)
            {
                string username = appUser.UserName?.ToLower() ?? "";
                if (!string.IsNullOrEmpty(username) && Lowered.Contains(username))
                {
                    return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {
                        Code = "passwordContainsUserName",
                        Description = "Password should not contain your username."
                    
                    }));
                }
            }
            var nationId=@"(?!([0-9])\1{9})[0-9]{10}";
            if (Regex.IsMatch(password, nationId))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "passwordContainsNationId",
                    Description = "Password should not contain a valid national ID code."
                
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }