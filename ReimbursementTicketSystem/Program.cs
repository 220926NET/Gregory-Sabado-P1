// See https://aka.ms/new-console-template for more information

AuthService authService = new AuthService();

int input;
System.Console.WriteLine("Reimbursement Ticket System");
while (authService.getCurrentUser() == null)
{
    System.Console.WriteLine("Press [1] to login or [2] to register an account");
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
            default:
                System.Console.WriteLine("Invalid input, press [1] to login or [2] to register an account");
                break;
        }
    }
}
