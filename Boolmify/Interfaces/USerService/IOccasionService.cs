    using Boolmify.Dtos.Occasion;

    namespace Boolmify.Interfaces.USerService;

    public interface IOccasionService
    {
        Task<List<OccasionDto>> GetOccasionsAsync();
    }