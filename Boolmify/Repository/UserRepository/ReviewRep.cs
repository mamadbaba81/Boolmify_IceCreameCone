    using AutoMapper;
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
    }

   