    using System.Collections;
    using Boolmify.Dtos.FAQ;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Boolmify.Repository.AdminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
    [ApiController]
    [Route("Api/Admin/FAQ")]
    [Authorize(Roles = "Admin")]
    public class AdminFAQController: ControllerBase
    {
        private readonly IAdminFAQService  _FAQService;

        public AdminFAQController(IAdminFAQService FAQService)
        {
            _FAQService = FAQService;
        }

        [HttpGet("GetAllFAQs")]
        public async Task<ActionResult<IEnumerable<FAQDto>>> GetAllFAQsAsync([FromQuery] string? search , [FromQuery] int pageNumbe = 1, [FromQuery] int pageSize = 10 )
        {
            var  result = await _FAQService.GetAllAsync(search, pageNumbe, pageSize);
            
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<FAQDto>> GetFAQByIDAsync(int id)
        {
            var faq = await _FAQService.GetByIdAsync(id);
            if (faq == null) return NotFound();
            return Ok(faq);
        }

        [HttpPost("CreateFAQ")]
        public async Task<ActionResult<FAQDto>> CreateFAQAsync( CreateFAQDto dto)
        {
            var faq = await _FAQService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetFAQByIDAsync), new { id = faq.FaqId }, faq);
            
        }

        [HttpPost("UpdateFAQ")]
        public async Task<ActionResult<FAQDto>> UpdateFAQAsync(int id , UpdateFAQDto dto)
        {
          if (!ModelState.IsValid) return BadRequest(ModelState);
          if(id != dto.FaqId) return BadRequest("ID mismatch");
          var updated = await _FAQService.UpdateAsync(dto);
          return Ok(updated);
        }

        [HttpDelete("DeleteFAQ/{id}")]
        public async Task<ActionResult> DeleteFAQAsync(int id)
        {
            var faq = await _FAQService.DeleteAsync(id);
            if (faq == null) return NotFound("FAQ not found");
            return Ok(faq);
        }

        [HttpPatch("toggleActive/{id}")]
        public async Task<ActionResult> ToggleActiveAsync(int id)
        {
            var faq = await _FAQService.ToogleActiveAsync(id);
            if (!faq) return NotFound("FAQ not found");
            return Ok("FAQ status toggled");
        }
        
        
    }