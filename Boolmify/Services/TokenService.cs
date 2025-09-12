    using System.IdentityModel.Tokens.Jwt;
         using System.Security.Claims;
         using System.Text;
         using Boolmify.Interfaces;
         using Boolmify.Models;
         using Microsoft.IdentityModel.Tokens;
     
         namespace Boolmify.Services;
     
         public class TokenService: ITokenService
         {
             private readonly IConfiguration _config;
     
             private readonly SymmetricSecurityKey _key;
     
             public TokenService(IConfiguration config)
             {
                 _config = config;
                 _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigninKey"]));
             }
         
             public string CreateToken(AppUser user)
             {
                 var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.NameIdentifier, user.Identifier),
     
                     new Claim(ClaimTypes.Role, user.Role.ToString()),
                 };
                 var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                 var tokenDescriptor = new SecurityTokenDescriptor
                 {
                     Subject = new ClaimsIdentity(claims),
                     Expires = DateTime.Now.AddDays(7),
                     SigningCredentials = creds,
                     Issuer = _config["JWT:Issuer"],
                     Audience = _config["JWT:Audience"]
                 };
                 var TokenHandeler = new JwtSecurityTokenHandler();
                 var token = TokenHandeler.CreateToken(tokenDescriptor);
                 return TokenHandeler.WriteToken(token);
             }
         }