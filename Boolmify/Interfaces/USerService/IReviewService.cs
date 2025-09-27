    using Boolmify.Dtos.Review;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;
  //DtaBase
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto> AddReviewAsync(int userId, CreateReviewDto dto);
        
        Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);
        
        Task<ReviewDto?> UpdateUserReviewAsync(int userId, UpdateReviewDto dto);
        
        Task<bool> DeleteUserReviewAsync(int userId, int reviewId);
    }