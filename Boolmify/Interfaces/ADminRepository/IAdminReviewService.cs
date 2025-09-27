    using Boolmify.Dtos.Review;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminReviewService
    {
        Task<ReviewDto> AddReviewAsync(CreateReviewDto  dto , int id);

        Task<ReviewDto> UpdateReviewAsync( UpdateReviewDto dto);

        Task<List<ReviewDto>> GetAllReviewsAsync();
        
        Task<List<ReviewDto>> GetReviewAsync(int productID);
        
        Task<bool>DeleteReviewAsync(int reviewId);
        
    }