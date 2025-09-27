    using System.Security.Claims;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
    [ApiController]
    [Route("Api/Admin/Reviews")]
    [Authorize(Roles = "Admin")]
    public class AdminReviewController: ControllerBase
    {
        private readonly IAdminReviewService _reviewService;

        public AdminReviewController(IAdminReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewAsync(int reviewId)
        {
            var  review = await _reviewService.GetReviewAsync(reviewId);
            if (review == null) return NotFound("Review not found");
            return Ok(review);
        }
       
        [HttpPost("AddReview")]
        public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var review = await _reviewService.AddReviewAsync(dto, userId);
            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReviewAsync(int reviewId, [FromBody] UpdateReviewDto dto)
        {
            if(reviewId != dto.ReviewId) return BadRequest("Id mismatch");
            var reviewToUpdate = await _reviewService.UpdateReviewAsync(dto);
            return Ok(reviewToUpdate);
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            var  reviewToDelete = await _reviewService.DeleteReviewAsync(reviewId);
            if (!reviewToDelete) return NotFound("Review not found");
            return NoContent();
        }
    }