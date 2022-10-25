using Models;
using DataAccess;

namespace BusinessLogic;
public interface ITicketService
{
    int Submit(Ticket ticket);

    Ticket UpdateTicket(int ticket_id, Status status);

    Ticket GetTicket(int id);

    List<Ticket> GetTickets(Status status);
    List<Ticket> GetTickets(int author);

}

public class TicketService : ITicketService
{
    private ITicketRepository ticketRepo;

    public TicketService(ITicketRepository ticketRepo)
    {
        this.ticketRepo = ticketRepo;
    }

    public int Submit(Ticket ticket)
    {
        return ticketRepo.Submit(ticket);

    }

    public Ticket GetTicket(int id)
    {
        return ticketRepo.GetTicket(id);
    }

    public List<Ticket> GetTickets(Status status)
    {
        return ticketRepo.GetTickets(status);

    }


    public Ticket UpdateTicket(int ticket_id, Status status)
    {
        Ticket ticket = ticketRepo.GetTicket(ticket_id);
        if (ticket.status == Status.Pending)
        {
            ticketRepo.UpdateTicket(ticket_id, status);
            return ticketRepo.GetTicket(ticket_id);
        }
        else
        {
            return ticket;
        }
    }

    public List<Ticket> GetTickets(int author)
    {
        return ticketRepo.GetTickets(author);
    }
}