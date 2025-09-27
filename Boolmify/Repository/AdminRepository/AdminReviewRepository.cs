    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Boolmify.Data;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminReviewRepository:IAdminReviewService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;

        public AdminReviewRepository(ApplicationDBContext context , IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<ReviewDto> AddReviewAsync(CreateReviewDto dto, int id)
        {
            var review = _mapper.Map<Review>(dto);
            review.UserId = id;
            review.CreatedAt = DateTime.Now;
            
            _Context.Reviews.Add(review);
            await _Context.SaveChangesAsync();
            
            var reviewwithUser = await _Context.Reviews.Include(r=>r.User)
                .FirstOrDefaultAsync(r=>r.UserId == id);
            return _mapper.Map<ReviewDto>(reviewwithUser);
        }

        public async Task<ReviewDto> UpdateReviewAsync(UpdateReviewDto dto)
        {
          var review = await _Context.Reviews.Include(r=>r.User).FirstOrDefaultAsync(r=>r.ReviewId == dto.ReviewId);
          if (review == null) return null;
          _mapper.Map(dto, review);
          review.UpdateAt = DateTime.Now;
          await _Context.SaveChangesAsync();
          return _mapper.Map<ReviewDto>(review);
        }

        public async Task<List<ReviewDto>> GetAllReviewsAsync()
        {
            return await _Context.Reviews.Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt).ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ReviewDto>> GetReviewAsync(int productId)
        {
            return await _Context.Reviews.Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt).Include(r => r.User)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
           var review = await _Context.Reviews.FindAsync(reviewId);
           if (review == null) return false;
           _Context.Reviews.Remove(review);
           await _Context.SaveChangesAsync();
           return true;
        }
    }