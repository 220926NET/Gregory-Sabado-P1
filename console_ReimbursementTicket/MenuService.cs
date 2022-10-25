using Models;
using DataAccess;
using BusinessLogic;
using System;
public class MenuService
{
    /// <summary>
    /// Handles Main Menu functionality, prompts user to login and then moves to ticket menu.
    /// User can logout which goes back to login, or user can exit which exits program entirely
    /// </summary>
    internal void DisplayMenu()
    {
        System.Console.WriteLine("Reimbursement Ticket System");

        User? user;
        bool exit = false;

        while (!exit)
        {
            user = LoginRegisterMenu();
            exit = TicketMenu(user);
        }
        Environment.Exit(0);
    }

    /// <summary>
    /// Calls AuthService's Login method under current user
    /// </summary>
    /// <returns>User object</returns>
    internal User? LoginMenu()
    {
        User user = new();
        AuthService authService = new AuthService(new AuthRepository());
        System.Console.WriteLine("Username:");
        string? username = Console.ReadLine();
        System.Console.WriteLine("Password:");
        string? password = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            user = authService.Login(username, password);

        return user;
    }

    /// <summary>
    /// Calls AuthService Register Method under current user
    /// </summary>
    internal void RegisterMenu()
    {
        AuthService authService = new AuthService(new AuthRepository());
        System.Console.WriteLine("Username:");
        string? username = Console.ReadLine();
        System.Console.WriteLine("Password:");
        string? password = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            authService.Register(username, password);
    }

    /// <summary>
    /// Calls TicketService Submit method under current user
    /// </summary>
    /// <param name="user">User object that is logged in</param>
    internal void SubmitTicketMenu(User user)
    {
        TicketService ticketService = new TicketService();
        ticketService.Submit(user);
    }

    /// <summary>
    /// Calls TicketService ViewSubmittedTicket method under current user
    /// </summary>
    /// <param name="user">User object that is logged in</param>
    internal void ViewSubmittedTicketsMenu(User user)
    {
        TicketService ticketService = new TicketService();
        ticketService.ViewSubmittedTickets(user);
    }

    /// <summary>
    /// Calls TicketService under current user
    /// </summary>
    /// <param name="user"></param>
    internal void ApproveTicketsMenu(User user)
    {
        TicketService ticketService = new TicketService();
        ticketService.ApproveTickets(user);
    }

    /// <summary>
    /// Prompts user to login to an account or register a new account (doesn't log in).
    /// User is stuck in menu until they login or exit program
    /// </summary>
    /// <returns></returns>
    internal User LoginRegisterMenu()
    {
        int input;

        User? currentUser = null;

        //  Login/Register
        while (currentUser == null)
        {
            System.Console.WriteLine("Press [1] to login, [2] to register an account, or [3] to exit.");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                switch (input)
                {
                    case 1:
                        currentUser = LoginMenu();
                        break;
                    case 2:
                        RegisterMenu();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine("Invalid input!");
                        break;
                }
            }
            else
            {
                System.Console.WriteLine("Please input a valid integer.");
            }
        }
        return currentUser;
    }


    /// <summary>
    /// Prompts user to submit ticket under their id, view their submitted tickets, logout, or exit the program
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    internal bool TicketMenu(User user)
    {
        bool exit = false, logout = false;
        int input;
        while (!logout)
        {
            switch (user.manager)
            {
                case false:
                    System.Console.WriteLine("Press [1] to submit a ticket, [2] to view submitted tickets, [3] to logout, or [4] to exit program");
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        switch (input)
                        {
                            case 1:
                                SubmitTicketMenu(user);
                                break;
                            case 2:
                                ViewSubmittedTicketsMenu(user);
                                break;
                            case 3:
                                logout = true;
                                break;
                            case 4:
                                logout = true;
                                exit = true;
                                break;
                            default:
                                System.Console.WriteLine("Invalid Input");
                                break;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Please input a valid integer.");
                    }
                    break;
                case true:
                    System.Console.WriteLine("Press [1] to submit a ticket, [2] to view submitted tickets, [3] to approve/deny submitted tickets, [4] to logout, or [5] to exit program");
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        switch (input)
                        {
                            case 1:
                                SubmitTicketMenu(user);
                                break;
                            case 2:
                                ViewSubmittedTicketsMenu(user);
                                break;
                            case 3:
                                ApproveTicketsMenu(user);
                                break;
                            case 4:
                                logout = true;
                                break;
                            case 5:
                                logout = true;
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
                    break;
            }
        }
        return exit;
    }
}