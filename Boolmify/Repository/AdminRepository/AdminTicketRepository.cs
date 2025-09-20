        using Boolmify.Data;
        using Boolmify.Dtos.Ticket;
        using Boolmify.Interfaces.ADminRepository;
        using Boolmify.Models;
        using Microsoft.EntityFrameworkCore;

        namespace Boolmify.Repository.AdminRepository;

        public class AdminTicketRepository :IAdminTicketService
        {
            private readonly ApplicationDBContext _Context;

            public AdminTicketRepository(ApplicationDBContext context)
            {
                _Context = context;
            }
            public async Task<IEnumerable<TicketDto>> GetAllAsync(TicketStatus? status =null, int? userId = null
                , string? search = null , int pageNumber = 1, int pageSize = 10)
            {
             var query =  _Context.Tickets.Include(t => t.User)
                 .Include(t => t.Order).Include(t=>t.Messages)
                 .AsQueryable();
             
             //filter bar asas karbar
             if(userId.HasValue) query = query.Where(t => t.UserId == userId);
             
             if (status.HasValue) query = query.Where(t=>t.Status ==  status);

             if (!string.IsNullOrWhiteSpace(search))
             {
                 query = query.Where(t => t.Subject.Contains(search) 
                || t.User.UserName.Contains(search) || t.Messages.Any(m=>m.Content.Contains(search))); 
             }

             return await query.OrderByDescending(t => t.CreatedAt).Skip((pageNumber - 1)* pageSize)
                 .Take(pageSize).Select(t=>new TicketDto
             {
                 TicketId = t.TicketId,
                 Subject = t.Subject,
                 Status = t.Status.ToString(),
                 CreatedAt = t.CreatedAt,
                 UserName = t.User.UserName,
                 OrderId = t.OrderId,
                 Messages= t.Messages.Select(m => new TicketMessageDto
                 {
                     MessageId = m.TicketMessageId,
                     TicketId = m.TicketId,
                     SenderId = m.SenderId,
                    SenderName  = m.Sender.UserName, // از رابطه User توی Message می‌گیریم
                    Content = m.Content,
                     CreatedAt = m.CreatedAt
                 }).ToList()
             }).ToListAsync();

            }

            public async Task<TicketDto?> GetById(int id)
            {
                var ticket = await _Context.Tickets.Include(t => t.User)
                    .Include(t => t.Order).Include(t=>t.Messages)
                    .ThenInclude(t=>t.Sender)
                    .FirstOrDefaultAsync(i => i.TicketId == id);
                if (ticket == null) return null;
                return new TicketDto
                {
                    TicketId = ticket.TicketId,
                    UserId = ticket.UserId,
                    UserName = ticket.User.UserName,
                    OrderId = ticket.OrderId,
                    Subject = ticket.Subject,
                    Status = ticket.Status.ToString(),
                    CreatedAt = ticket.CreatedAt,
                    Messages = ticket.Messages.Select(m => new TicketMessageDto
                    {
                        MessageId = m.TicketMessageId,
                        TicketId = m.TicketId,
                        SenderId = m.SenderId,
                        SenderName = m.Sender.UserName,
                        Content = m.Content,
                        CreatedAt = m.CreatedAt
                    }).ToList()
                };
            }
            
            public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
            {
                var ticket = new Ticket
                {
                    UserId = dto.UserId,
                    OrderId = dto.OrderId,
                    Subject = dto.Subject,
                    Status = TicketStatus.Open,
                    CreatedAt = DateTime.UtcNow,
                };
                _Context.Tickets.Add(ticket);
                await _Context.SaveChangesAsync();
                return  new TicketDto
                {
                    TicketId = ticket.TicketId,
                    UserId = ticket.UserId,
                    UserName = (await _Context.Users.FindAsync(ticket.UserId))?.UserName ?? "",
                    OrderId = ticket.OrderId,
                    Subject = ticket.Subject,
                    Status = ticket.Status.ToString(),
                    CreatedAt = ticket.CreatedAt,
                    Messages = new List<TicketMessageDto>() // تیکت تازه ساخته شده، پس خالیه
                };
            }

            public async Task<TicketMessageDto> ReplyAsync(int ticketId, CreateTicketMessageDto dto)
            {
                var ticket = await _Context.Tickets.Include(t => t.User)
                    .Include(t=>t.Messages).FirstOrDefaultAsync(t=>t.TicketId == ticketId);
                if (ticket == null) throw new Exception("Ticket not found");
                var message = new TicketMessage
                {
                    TicketId = ticketId,
                    SenderId = dto.SenderId,
                    Content = dto.Content,
                    CreatedAt = DateTime.Now
                };
                ticket.Messages.Add(message);
                await _Context.SaveChangesAsync();
                var sender = await _Context.Users.FindAsync(dto.SenderId);

                return new TicketMessageDto
                {
                    MessageId = message.TicketMessageId,
                    TicketId = message.TicketId,
                    SenderId = message.SenderId,
                    SenderName = sender?.UserName ?? "Unknown",
                    Content = message.Content,
                    CreatedAt = message.CreatedAt
                };
            }

            public async Task<TicketDto> UpdateAsync(UpdateTicketStatusDto dto)
            {
               var ticket = await _Context.Tickets.Include(t => t.User)
                   .Include(t=>t.Messages).ThenInclude(t=>t.Sender).FirstOrDefaultAsync(t=>t.TicketId==dto.TicketId);
               if(ticket==null) throw new Exception("Ticket not found");
               ticket.Status = dto.Status;
               ticket.UpdateAt = DateTime.Now;
               await _Context.SaveChangesAsync();

               return new TicketDto
               {
                   TicketId = ticket.TicketId,
                   UserId = ticket.UserId,
                   UserName = ticket.User.UserName,
                   OrderId = ticket.OrderId,
                   Subject = ticket.Subject,
                   Status = ticket.Status.ToString(),
                   CreatedAt = ticket.CreatedAt,
                   Messages = ticket.Messages.Select(m => new TicketMessageDto
                   {
                       MessageId = m.TicketMessageId,
                       TicketId = m.TicketId,
                       SenderId = m.SenderId,
                       SenderName = m.Sender.UserName,
                       Content = m.Content,
                       CreatedAt = m.CreatedAt
                   }).ToList()
               };

            }       

            public async Task<bool> DeleteAsync(int id)
            {
                var ticket = await _Context.Tickets.Include(t => t.Messages)
                    .Include(t=>t.User)
                    .FirstOrDefaultAsync(t=>t.TicketId == id);
              if(ticket == null) return false;
              _Context.TicketMessages.RemoveRange(ticket.Messages);
              _Context.Tickets.Remove(ticket);
              await _Context.SaveChangesAsync();
              return true;
            }
        }