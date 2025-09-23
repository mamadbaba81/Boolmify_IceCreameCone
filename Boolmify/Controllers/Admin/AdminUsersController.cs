    using Boolmify.Dtos.Account;
    using Boolmify.Helper;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
    [ApiController]
    [Route("Api/Admin/Users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController: ControllerBase
    {
       private readonly IAdminUserService  _UserService;

       public AdminUsersController(IAdminUserService adminUserService)
       {
           _UserService = adminUserService;
       }
       
        [HttpGet("GetAll")]
       public async Task<ActionResult<QueryObject<UserDto>>> GetAllUsers([FromQuery] string? search , [FromQuery] int pageNumbe = 1, [FromQuery] int pageSize = 10)
       {
           var result = await _UserService.GetAllAsync(search, pageNumbe, pageSize);
           return Ok(result);
       }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UserDto>> GetUserByID([FromRoute] int id)
        {
            var user = await _UserService.GetByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = await _UserService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetUserByID), new {id = user.Id}, user);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id ,[FromBody] UpdateUserDto dto  )
        {
            var  user = await _UserService.UpdateAsync(id, dto);
            return Ok(user);
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _UserService.DeleteAsync(id);
            if(!user) return BadRequest("User not found");
            return NoContent();
            
        }

        [HttpPut("ChangeRole{id}")]
        public async Task<ActionResult> ChangeRole([FromQuery] int userId, [FromQuery] string newRole)
        {
            var user = await _UserService.ChangeRoleAsync(userId ,  newRole);
            if(!user) return BadRequest("Role not changed");
            return Ok(true);
        }

        [HttpPut("Change/Password{id}")]
        public async Task<ActionResult> ChangePassword([FromQuery] int userId, [FromQuery] string newPassword)
        {
            var user = await _UserService.ChangePasswordAsync(userId, newPassword);
            if(!user) return BadRequest("Password not changed");
            return Ok(true);
        }
        
    }