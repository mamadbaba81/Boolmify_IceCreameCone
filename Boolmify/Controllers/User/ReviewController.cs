    using System.Security.Claims;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/Reviews")]
    
    public class ReviewController: ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("AddReview")]
        public async Task<ActionResult<ReviewDto>> AddReview([FromBody] CreateReviewDto dto)
        {
            var  userId = GetUserId();
            var review = await _reviewService.AddReviewAsync(userId, dto);
            return Ok(review);
        }

        [HttpGet("GetReviews/{id}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews(int id)
        {
            var reviews = await _reviewService.GetReviewsForProductAsync(id);
            return Ok(reviews);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int id, [FromBody] UpdateReviewDto dto)
        {
            if(id != dto.ReviewId) return BadRequest("Id mismatch");
             var userId = GetUserId();
             var update = await _reviewService.UpdateUserReviewAsync(userId, dto);
             if(update == null) return BadRequest("Review not found or not Yours");
             return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var userId = GetUserId();
            var success = await _reviewService.DeleteUserReviewAsync(userId, id);
            if(!success) return NotFound("Review not found or not Yours");
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews =await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }
    }