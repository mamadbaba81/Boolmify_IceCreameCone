    using System.IdentityModel.Tokens.Jwt;
         using System.Security.Claims;
         using System.Security.Cryptography;
         using System.Text;
         using Boolmify.Data;
         using Boolmify.Dtos.RefreshToken;
         using Boolmify.Interfaces;
         using Boolmify.Models;
         using Microsoft.AspNetCore.Identity;
         using Microsoft.IdentityModel.Tokens;
     
         namespace Boolmify.Services;
     
         public class TokenService: ITokenService
         {
             private readonly IConfiguration _config;
     
             private readonly SymmetricSecurityKey _key;
             
             private readonly UserManager<AppUser> _UserManager;
             
             private readonly ApplicationDBContext  _Context;
     
             public TokenService(IConfiguration config ,  UserManager<AppUser> userManager , ApplicationDBContext context)
             {
                 _config = config;
                 _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigninKey"]));
                 _UserManager = userManager;
                 _Context = context;
                 
             }
             
             private string GenerateRefreshToken()
             {
                 var randomNumber = new byte[32];
                 using var rng = RandomNumberGenerator.Create();
                 rng.GetBytes(randomNumber);
                 return Convert.ToBase64String(randomNumber);
             }
         
             public async Task<(string AccessToken, string RefreshToken)> CreateToken(AppUser user)
             {
                 var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.NameIdentifier, user.Identifier),
     
                     new Claim(ClaimTypes.Name, user.UserName ?? user.Identifier),
                 };
                 var roles = await _UserManager.GetRolesAsync(user);
                 foreach (var role in roles)
                 {
                     claims.Add(new Claim(ClaimTypes.Role, role));
                 }
                 var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                 var tokenDescriptor = new SecurityTokenDescriptor
                 {
                     Subject = new ClaimsIdentity(claims),
                     Expires = DateTime.Now.AddMinutes(15),
                     SigningCredentials = creds,
                     Issuer = _config["JWT:Issuer"],
                     Audience = _config["JWT:Audience"]
                 };
                 var TokenHandeler = new JwtSecurityTokenHandler();
                 var token = TokenHandeler.CreateToken(tokenDescriptor);
                 var AccessToken = TokenHandeler.WriteToken(token);
                 var refreshToken = GenerateRefreshToken();
                 _Context.RefreshTokens.Add(new RefreshToken
                 {
                     Token = refreshToken,
                     Expires = DateTime.Now.AddDays(7),
                     UserId = user.Id
                 });
                 await _Context.SaveChangesAsync();
                 
                 return (AccessToken, refreshToken);
             }
             
         }