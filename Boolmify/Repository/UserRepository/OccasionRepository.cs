    using Boolmify.Data;
    using Boolmify.Dtos.Occasion;
    using Boolmify.Interfaces.USerService;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class OccasionRepository: IOccasionService
    {
        private readonly ApplicationDBContext  _Context;

        public OccasionRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        
        public async Task<List<OccasionDto>> GetOccasionsAsync()
        {
            var Occasions = await _Context.Occasions.Where(o => o.IsActive).OrderBy(o => o.CreateAt).ToListAsync();
            return  Occasions.Select(o=>new  OccasionDto
            {
                OccasionId = o.OccasionId,
                Name = o.Name,
                description = o.Description,
                IconUrl = o.IconUrl,
                IsActive = o.IsActive
            }).ToList();
        }
    }