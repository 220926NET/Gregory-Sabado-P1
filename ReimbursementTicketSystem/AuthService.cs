//handles interactions with user, and authrepository i.e. prompting user to login then sending login info to authrepository

public class AuthService
{
    private AuthRepository authRepository = new AuthRepository();

    //prompts user input
    //outputs validation of successful login
    public void login()
    {
        System.Console.WriteLine("Email:");
        string? email = Console.ReadLine();
        System.Console.WriteLine("Password:");
        string? password = Console.ReadLine();
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            if (authRepository.login(email, password))
            {
                System.Console.WriteLine("Successful login! Hello {0}", getCurrentUser().username);
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input");
        }

    }
    public void register()
    {

        System.Console.WriteLine("Enter an Email:");
        string? email = Console.ReadLine();
        System.Console.WriteLine("Enter a Username:");
        string? username = Console.ReadLine();
        System.Console.WriteLine("Enter a Password:");
        string? pwd = Console.ReadLine();
        System.Console.WriteLine("Confirm Password:");
        string? pwd_cnfm = Console.ReadLine();
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pwd) && pwd.CompareTo(pwd_cnfm) == 0)
        {
            //TODO: check for email syntax
            if (authRepository.register(username, email, pwd))
            {
                System.Console.WriteLine($"Successful Registration! Proceed to login to access account");
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input");
        }


    }

    public User getCurrentUser()
    {
        return authRepository.getCurrentUser();
    }

}