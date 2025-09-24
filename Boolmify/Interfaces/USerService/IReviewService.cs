    using Boolmify.Dtos.Review;

    namespace Boolmify.Interfaces.USerService;

    public interface IReviewService
    {
        Task<ReviewDto> AddReviewAsync(int id, CreateReviewDto dto);
        
        Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int id);
    }