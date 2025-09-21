    using System.Collections;
    using Boolmify.Dtos.Ticket;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
        [ApiController]
        [Route("api/Admin/Ticket")]
        [Authorize(Roles = "Admin")]
    public class AdminTicketController: ControllerBase
    {
        private readonly IAdminTicketService _ticketService;

        public AdminTicketController(IAdminTicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTicketsAsync([FromQuery]TicketStatus?  status =null,[FromQuery] int? userId = null
               ,  [FromQuery]  string? search = null ,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var tickets = await _ticketService.GetAllAsync(status, userId, search, pageNumber, pageSize);
            return Ok(tickets);
        }

        [HttpGet("GetTicketById/{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketByIdAsync(int id)
        {
            var Ticket = await _ticketService.GetById(id);
            if (Ticket==null) return NotFound("Ticket not found");
            return Ok(Ticket);
        }

        [HttpPost("CreateTicket")]
        public async Task<ActionResult<TicketDto>> CreateTicketAsync(int id , [FromBody] CreateTicketDto dto)
        {
            var Ticket = await _ticketService.CreateAsync(dto);
            if (Ticket == null) return NotFound("Ticket creation failed");
            return Ok(Ticket);
        }

        [HttpPost("{TicketId}/Reply")]
        public async Task<ActionResult<TicketMessageDto>> ReplyAsync(int TicketId, [FromBody] CreateTicketMessageDto dto)
        {
            var reply = await _ticketService.ReplyAsync(TicketId, dto);
            if (reply == null) return NotFound("Ticket not found");
            return Ok(reply);
        }

        [HttpPut("UpdateTicket")]
        public async Task<ActionResult<TicketDto>> UpdateTicketAsync([FromBody]UpdateTicketStatusDto dto)
        {
            var  Ticket = await _ticketService.UpdateAsync(dto);
            if (Ticket == null) return NotFound("Ticket not found");
            return Ok(Ticket);
        }

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<ActionResult<TicketDto>> DeleteTicketAsync(int id)
        {
            var Ticket = await _ticketService.DeleteAsync(id);
            if (!Ticket) return NotFound("Ticket not found");
            return NoContent();
        }
    }