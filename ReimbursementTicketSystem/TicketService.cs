public class TicketService
{
    private TicketRepository tr = new TicketRepository();
    public void Submit(User currentUser)
    {
        System.Console.WriteLine("Enter amount to be reimbursed:");
        uint amount;
        if (uint.TryParse(Console.ReadLine(), out amount))
        {
            System.Console.WriteLine("Enter description for ticket:");
            string? desc = Console.ReadLine();
            if (!String.IsNullOrEmpty(desc))
            {
                uint ticketID = tr.Submit(amount, desc, currentUser);
                System.Console.WriteLine($"Ticket submission successful, your ticket is #{ticketID} in line");

            }
            else
            {
                System.Console.WriteLine("Invalid description inputted");
            }
        }
        else
        {
            System.Console.WriteLine("Invalid amount inputted.");
        }

    }

    public void ViewTickets(User user)
    {
        foreach (Ticket ticket in user.tickets)
        {
            System.Console.WriteLine(ticket);
        }

    }
}