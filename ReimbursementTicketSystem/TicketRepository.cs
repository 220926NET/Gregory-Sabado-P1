public class TicketRepository
{
    //id of ticket, and the ticket
    private Queue<Tuple<uint, Ticket>> database = new Queue<Tuple<uint, Ticket>>();
    private uint currentID = 1;

    public uint Submit(uint amount, string desc, User owner)
    {
        Ticket ticket = new Ticket(currentID, amount, desc, owner);
        database.Enqueue(new Tuple<uint, Ticket>(currentID, ticket));
        owner.tickets.Add(ticket);
        return currentID++;

    }

    public void ViewSubmittedTickets()
    {

    }

    public void ApproveTickets()
    {

    }

}