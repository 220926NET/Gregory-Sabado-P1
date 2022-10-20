public class TicketService
{
    private TicketRepository tr;

    public TicketService()
    {
        this.tr = new TicketRepository();
    }
    public void Submit(User currentUser)
    {
        System.Console.WriteLine("Enter amount to be reimbursed:");
        decimal amount;
        if (decimal.TryParse(Console.ReadLine(), out amount))
        {
            System.Console.WriteLine("Enter description for ticket:");
            string? desc = Console.ReadLine();
            if (!String.IsNullOrEmpty(desc))
            {
                int ticketID = tr.Submit(amount, desc, currentUser);
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

    public void ApproveTickets(User user)
    {
        if (user.manager)
        {
            Queue<Ticket> tickets = tr.GetPendingTickets();
            List<Ticket> reviewed = new List<Ticket>();
            Ticket? next;
            bool exit = false;
            while (!exit && tickets.TryPeek(out next))
            {
                System.Console.WriteLine(next);
                System.Console.WriteLine("Press [1] to Approve, [2] to Deny, or [3] to exit");
                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            ApproveTicket(next);
                            reviewed.Add(tickets.Dequeue());
                            break;
                        case 2:
                            DenyTicket(next);
                            reviewed.Add(tickets.Dequeue());
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            System.Console.WriteLine("Invalid input.");
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Please input a valid integer.");
                }
            }
            if (!tickets.TryPeek(out next))
            {
                System.Console.WriteLine("All current tickets have been reviewed.");
            }
            UpdateTickets(reviewed);
        }
        else
        {
            System.Console.WriteLine("Not a manager.");
        }
    }

    public void ViewSubmittedTickets(User currentUser)
    {
        List<Ticket> tickets = tr.GetSubmittedTickets(currentUser);
        foreach (Ticket ticket in tickets)
        {
            System.Console.WriteLine(ticket);
        }
    }

    internal void UpdateTickets(List<Ticket> tickets)
    {
        tr.UpdateTickets(tickets);
    }


    bool ApproveTicket(Ticket ticket)
    {
        if (ticket.status == Status.Pending)
        {
            System.Console.WriteLine("Approved!");
            ticket.status = Status.Approved;
            return true;
        }
        return false;
    }
    bool DenyTicket(Ticket ticket)
    {
        if (ticket.status == Status.Pending)
        {
            System.Console.WriteLine("Denied!");
            ticket.status = Status.Denied;
            return true;
        }
        return false;
    }


}