// See https://aka.ms/new-console-template for more information

AuthService authService = new AuthService();
TicketService ticketService = new TicketService();

int input;
System.Console.WriteLine("Reimbursement Ticket System");
//  Login/Register
while (authService.getCurrentUser() == null)
{
    System.Console.WriteLine("Press [1] to login, [2] to register an account, or [3] to exit.");
    if (int.TryParse(Console.ReadLine(), out input))
    {
        switch (input)
        {
            case 1:
                authService.login();
                break;
            case 2:
                authService.register();
                break;
            case 3:
                Environment.Exit(0);
                break;
            default:
                System.Console.WriteLine("Invalid input!");
                break;
        }
    }
}
//  Submit Ticket/Logout?
//  Determine whether Manager/Employee is current user
bool Exit = false;
User currentUser = authService.getCurrentUser();
while (!Exit)
{
    switch (currentUser.userPosition)
    {
        case UserPosition.Employee:
            System.Console.WriteLine("Press [1] to submit a ticket, [2] to view your submitted tickets or [3] to exit");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                switch (input)
                {
                    case 1:
                        ticketService.Submit(currentUser);
                        break;
                    case 2:
                        ticketService.ViewTickets(currentUser);
                        break;
                    case 3:
                        Exit = true;
                        break;
                    default:
                        System.Console.WriteLine("Invalid Input");
                        break;
                }
            }
            break;
        case UserPosition.Manager:
            System.Console.WriteLine("Press [1] to submit a ticket, [2] to view your submitted tickets, [3] to approve submitted tickets or [4] to exit");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                switch (input)
                {
                    case 1:
                        ticketService.Submit(currentUser);
                        break;
                    case 2:
                        ticketService.ViewTickets(currentUser);
                        break;
                    case 3:
                        Exit = true;
                        break;
                    default:
                        break;
                }
            }
            break;
    }
}
Environment.Exit(0);