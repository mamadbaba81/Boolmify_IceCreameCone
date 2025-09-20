    using Boolmify.Dtos.Ticket;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminTicketService
    {
        Task<IEnumerable<TicketDto>> GetAllAsync(TicketStatus? status =null, int? userId = null
            , string? search = null , int pageNumber = 1, int pageSize = 10);
        
        Task<TicketDto?> GetById(int id);
        
        Task<TicketDto> CreateAsync(CreateTicketDto dto);
        
        Task<TicketMessageDto> ReplyAsync(int ticketId, CreateTicketMessageDto dto);
        
        Task<TicketDto> UpdateAsync(UpdateTicketStatusDto  dto);
        Task<bool> DeleteAsync(int id);
        
    }