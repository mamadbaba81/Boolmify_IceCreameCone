    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using Boolmify.Dtos.Account;
    using Boolmify.Interfaces;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Controllers;
[Route("Api/Account")]
[ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        
        public AccountController(UserManager<AppUser> userManager , ITokenService tokenService , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            
        }
           [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
          if (!ModelState.IsValid)
              return BadRequest(ModelState);

          var identity = loginDto.Identifier.Trim().ToLower();
          
          var user = await _userManager.Users.FirstOrDefaultAsync
              (u=> u.Identifier.ToLower() ==  identity || u.Email.ToLower() == identity || u.PhoneNumber == identity);
          if (user == null)
              return Unauthorized("Invalid username ");
          var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);
          if (!result.Succeeded)
          {
              return Unauthorized("Username not Found and/or password  incorrect");
          }
          var roles = await _userManager.GetRolesAsync(user);
          
          var (accessToken , refreshToken) = await _tokenService.CreateToken(user);
          
          return Ok(new NewUserDto
          {
              Identifier = user.Identifier,
              Role = roles.FirstOrDefault() ??"User",
             AccessToken = accessToken,
             RefreshToken = refreshToken,
          });
            
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = registerDto.Identifier,
                    Identifier = registerDto.Identifier,
                    Role = UserRole.Customer
                };
                if (registerDto.Identifier.Contains("@"))
                {
                    if(!new EmailAddressAttribute().IsValid(registerDto.Identifier))
                        return BadRequest("Email address is not valid");
                    appUser.Email = registerDto.Identifier;
                }
                else
                {
                    if(!Regex.IsMatch(registerDto.Identifier, @"^(?!([0-9])\1{9})09[0-9]{9}$"))
                        return BadRequest("Invalid Phone number");
                    appUser.PhoneNumber = registerDto.Identifier;
                }
                var ExistingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Identifier == registerDto.Identifier.ToLower());
                if (ExistingUser != null)
                    return BadRequest("Username already exists");
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(appUser);
                        var (accessToken, refreshToken) = await _tokenService.CreateToken(appUser);
                        return Ok(new NewUserDto
                        {
                            Identifier = appUser.Identifier,
                            Role = roles.FirstOrDefault() ??"User",
                            AccessToken = accessToken,
                            RefreshToken = refreshToken,
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
        