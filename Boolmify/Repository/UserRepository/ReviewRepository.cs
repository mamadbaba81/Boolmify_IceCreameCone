    using Boolmify.Data;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.USerService;

    namespace Boolmify.Repository.UserRepository;

    public class ReviewRepository:IReviewService
    {
        private readonly ApplicationDBContext  _Context;

        public ReviewRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public Task<ReviewDto> AddReviewAsync(int id, CreateReviewDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int id)
        {
            throw new NotImplementedException();
        }
    }