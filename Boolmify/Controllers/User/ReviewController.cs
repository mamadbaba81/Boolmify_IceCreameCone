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
        [Authorize(Roles = "Customer , Admin")]
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
    }