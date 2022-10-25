using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLogic;
namespace api_ReimbursementTicketSystem.Controllers
{
    public class TicketController : ControllerBase
    {
        private ITicketService ticketService;
        private IAuthService authService;
        public TicketController(IAuthService authService, ITicketService ticketService)
        {
            this.authService = authService;
            this.ticketService = ticketService;
        }


        [HttpPost]
        [Route("tickets/")]
        public ActionResult Submit(Ticket ticket)
        {
            if (ticket.amount <= 0m)
            {
                return BadRequest("Amount must be greater than 0");
            }
            else if (string.IsNullOrWhiteSpace(ticket.desc))
            {
                return BadRequest("Description must be provided");
            }
            else
            {
                int ticket_id = -1;
                ticket_id = ticketService.Submit(ticket);
                if (ticket_id != -1)
                    return Created("", ticket_id);
                else
                    return BadRequest("Invalid ticket submission");
            }
        }


        [HttpPut]
        [Route("tickets/{ticket_id}")]
        public ActionResult UpdateTicket(int ticket_id, Status status, int current_id)
        {
            User current_user = authService.GetUser(current_id);
            Ticket ticket = ticketService.GetTicket(ticket_id);
            if (ticket.status != 0)
            {
                return BadRequest("Ticket already processed");
            }
            else if (current_id == ticket.author)
            {
                return BadRequest("User cannot process their own ticket");
            }
            else if (current_user.manager)
            {
                ticket = ticketService.UpdateTicket(ticket_id, status);
                return Accepted(ticket);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Employee cannot process tickets.");
            }
        }


        [HttpGet]
        [Route("tickets/")]
        public ActionResult GetPendingTickets(Status status)
        {
            List<Ticket> tickets = ticketService.GetTickets(status);
            return Ok(tickets);
        }

        [HttpGet]
        [Route("tickets/submitted")]
        public ActionResult GetSubmittedTickets(int author, int current_id)
        {
            if (author == current_id)
            {
                List<Ticket> tickets = ticketService.GetTickets(author);
                return Ok(tickets);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "User cannot see the submitted tickets of others.");
            }
        }


    }
}
