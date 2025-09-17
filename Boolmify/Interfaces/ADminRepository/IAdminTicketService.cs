    using Boolmify.Dtos.Ticket;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminTicketService
    {
        Task<IEnumerable<TicketDto>> GetAllAsync(string? status = null , int? userId = null);
        
        Task<TicketDto?> GetById(int id);
        
        Task<TicketDto> CreateAsync(CreateTicketDto dto);
        
        Task<TicketMessageDto> ReplyAsync(int ticketId, CreateTicketMessageDto dto);
        
        Task<TicketDto> UpdateAsync(UpdateTicketStatusDto  dto);
        Task<bool> DeleteAsync(int id);
        
    }