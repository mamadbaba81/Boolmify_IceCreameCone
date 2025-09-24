    using System.Security.Claims;
    using Boolmify.Dtos.Account;
    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/USer")]
    [Authorize(Roles = "User,Admin")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet("Profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var UserId =  GetUserId();
            var profile = await _userService.GetProfileAsync(UserId);
            if (profile == null) return NotFound("User not found");
            return Ok(profile);
        }

        [HttpPut("Profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            var UserId =  GetUserId();
            var update = await _userService.UpdateProfileAsync(UserId, dto);
            if (update == null) return NotFound("User not found");
            return Ok(update);
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePassDto dto)
        {
            var UserId =  GetUserId();
            var resualt = await _userService.ChangePasswordAsync(UserId , dto);
            if (!resualt) return BadRequest("Password change failed. Current password might be incorrect.");
            return Ok(resualt);
        }
        
    }