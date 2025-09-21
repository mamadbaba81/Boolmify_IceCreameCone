        using Boolmify.Dtos.Occasion;
        using Boolmify.Interfaces.ADminRepository;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;

        namespace Boolmify.Controllers;
        [ApiController]
        [Route("api/Occassion")]
        [Authorize(Roles = "Admin")]
        public class AdminOccassionController:ControllerBase
        {
            private readonly IAdminOccasionService  _occasionService;

            public AdminOccassionController(IAdminOccasionService occasionService)
            {
                _occasionService = occasionService;
            }
            
            [HttpGet("GetAllOccassions")]
            public async Task<ActionResult<IEnumerable<OccasionDto>>> GetAllOccassionsAsync (string? search = null, int pageNumber = 1, int pageSize = 10)
            {
                var occasions = await _occasionService.GetAllOccasionsAsync(search, pageNumber, pageSize);
                return Ok(occasions);
                
            }

            [HttpGet("GetOccasionById")]
            public async Task<ActionResult<OccasionDto>> GetOccasionByIdAsync(int id)
            {
                var occasion = await _occasionService.GetByIdAsync(id);
                if (occasion == null) return NotFound("Occasion not found");
                return Ok(occasion);
            }

            [HttpPost("CreateOccasion")]
            public async Task<ActionResult<OccasionDto>> CreateOccasionAsync([FromBody]CreateOccasionDto dto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var occasion = await _occasionService.CreateAsync(dto);
                if (occasion == null) return BadRequest("Occasion creation failed.");
                return Ok(occasion);
            }

            [HttpPut("UpdateOccasion")]
            public async Task<ActionResult<OccasionDto>> UpdateOccasionAsync(int id, [FromBody] UpdateOccasionDto dto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (id != dto.OccasionId)  return BadRequest("Occasion ID mismatch");
                var updated = await _occasionService.UpdateAsync(dto);
                if (updated == null) return BadRequest("Occasion not found");
                return Ok(updated);
            }

            [HttpPatch("ToggleActiveOccasion")]
            public async Task<ActionResult<OccasionDto>> ToggleActiveOccasionAsync(int id)
            {
                var occasion = await _occasionService.ToggleActiveAsync(id);
                if (occasion == null) return BadRequest("Occasion not found");
                return Ok("Occasion status updated");
            }

            [HttpDelete("DeleteOccasion/{id}")]
            public async Task<IActionResult> DeleteOccasionAsync(int id)
            {
                var  occasion = await _occasionService.DeleteOccasionAsync(id);
                if (occasion == null) return BadRequest("Occasion not found");
                return Ok("Occasion deleted successfully");
            }

        }