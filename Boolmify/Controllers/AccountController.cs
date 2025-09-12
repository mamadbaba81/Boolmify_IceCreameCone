    using Boolmify.Dtos.Account;
    using Boolmify.Interfaces;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Controllers;
[Route("api/Account")]
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

          var identity = loginDto.Identifier.Trim();
          var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.Identifier == loginDto.Identifier.ToLower());
          if (user == null)
              return Unauthorized("Invalid username ");
          var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);
          if (!result.Succeeded)
          {
              return Unauthorized("Username not Found and/or password  incorrect");
          }
          return Ok(new NewUserDto()
          {
              Identifier = user.Identifier,
              Role = user.Role.ToString(),
              Token = _tokenService.CreateToken(user)
          });
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser()
                {
                    UserName = registerDto.Identifier,
                    Role = UserRole.Customer
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            Identifier = appUser.Identifier,
                            Role = appUser.Role.ToString(),
                            Token = _tokenService.CreateToken(appUser)


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