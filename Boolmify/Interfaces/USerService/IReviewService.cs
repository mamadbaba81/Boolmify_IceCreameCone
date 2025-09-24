    using Boolmify.Dtos.Review;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;
  //DtaBase
    public interface IReviewService
    {
        
        Task<ReviewDto> AddReviewAsync(int userId, CreateReviewDto dto);
        
        Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);
        
        
    }