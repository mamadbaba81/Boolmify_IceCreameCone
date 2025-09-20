    using Boolmify.Dtos.Occasion;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminOccasionService
    {
        Task<IEnumerable<OccasionDto>> GetAllOccasionsAsync(string? search = null, int pageNumber = 1, int pageSize = 10);
        
        Task<OccasionDto?> GetByIdAsync(int id);
        
        Task<OccasionDto> CreateAsync(CreateOccasionDto dto);
        Task<OccasionDto> UpdateAsync(UpdateOccasionDto dto);
        
        Task<bool> DeleteOccasionAsync(int id);
        
        Task<bool> ToggleActiveAsync(int id );
        
    }