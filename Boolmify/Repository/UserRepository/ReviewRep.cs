    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Boolmify.Data;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class ReviewRep:IReviewService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;

        public ReviewRep(ApplicationDBContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            return await _Context.Reviews
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ReviewDto> AddReviewAsync(int userId, CreateReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            review.UserId = userId;
            review.CreatedAt = DateTime.Now;
            await _Context.Reviews.AddAsync(review);
            await _Context.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId)
        {
           var  reviews = _Context.Reviews.Include(r=>r.User)
               .Where(r=>r.ProductId == productId).OrderByDescending(r=>r.CreatedAt).ToListAsync();
           return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto?> UpdateUserReviewAsync(int userId, UpdateReviewDto dto)
        {
            var review = await _Context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == dto.ReviewId && r.UserId == userId);

            if (review == null) return null;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            review.UpdateAt = DateTime.Now;

            await _Context.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> DeleteUserReviewAsync(int userId, int reviewId)
        {
          
            var review = await _Context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId && r.UserId == userId);

            if (review == null) return false;

            _Context.Reviews.Remove(review);
            await _Context.SaveChangesAsync();
            return true;
        }
    }

   