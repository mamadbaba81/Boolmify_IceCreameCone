    using Boolmify.Data;
    using Boolmify.Dtos.Occasion;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminOccasionRepository:IAdminOccasionService
    {
        private readonly ApplicationDBContext  _Context;

        public AdminOccasionRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<OccasionDto>> GetAllOccasionsAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _Context.Occasions.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(o => o.Name.Contains(search)|| o.Description.Contains(search));

            return await query.OrderByDescending(o => o.CreateAt)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(o => new OccasionDto
                {
                    IsActive = o.IsActive,
                    OccasionId = o.OccasionId,
                    Name = o.Name,
                    description = o.Description,
                    IconUrl = o.IconUrl
                }).ToListAsync();
            
        }

        public async Task<OccasionDto?> GetByIdAsync(int id)
        {
            var occasion = await _Context.Occasions.FindAsync(id);
            if (occasion == null) return null;
            return new OccasionDto
            {
                IsActive = occasion.IsActive,
                OccasionId = occasion.OccasionId,
                Name = occasion.Name,
                description = occasion.Description,
                IconUrl = occasion.IconUrl
            };
        }

        public async Task<OccasionDto> CreateAsync(CreateOccasionDto dto)
        {
            var occasion = new Occasion
            {
                Name = dto.Name,
                Description = dto.description,
                IconUrl = dto.IconUrl,
                IsActive = true,
                CreateAt = DateTime.Now
            };
            await _Context.Occasions.AddAsync(occasion);
            await _Context.SaveChangesAsync();
            return await GetByIdAsync(occasion.OccasionId) ?? throw new Exception("Occasion creation failed");
        }

        public async Task<OccasionDto> UpdateAsync(int id ,UpdateOccasionDto dto)
        {
            var occasion = await _Context.Occasions.FindAsync(id);
            if (occasion == null) return null;
            if (!string.IsNullOrWhiteSpace(dto.Name)) occasion.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.description)) occasion.Description = dto.description;
            if (!string.IsNullOrWhiteSpace(dto.IconUrl)) occasion.IconUrl = dto.IconUrl;
            await _Context.SaveChangesAsync();
            return await GetByIdAsync(occasion.OccasionId) ??  throw new Exception("Occasion updation failed");

        }

        public async Task<bool> DeleteOccasionAsync(int id)
        {
            var occasion = await _Context.Occasions.FindAsync(id);
            if (occasion == null) return false;
            _Context.Occasions.Remove(occasion);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id)
        {
            var occasion = await _Context.Occasions.FindAsync(id);
            if (occasion == null) return false;
            occasion.IsActive = !occasion.IsActive;
            await _Context.SaveChangesAsync();
            return true;
        }
    }