//handles interactions with user, and authrepository i.e. prompting user to login then sending login info to authrepository

public class AuthService
{
    private AuthRepository authRepository = new AuthRepository();

    //prompts user input
    //outputs validation of successful login
    public User? Login()
    {
        System.Console.WriteLine("Email/Username:");
        string? email = Console.ReadLine();
        System.Console.WriteLine("Password:");
        string? password = Console.ReadLine();
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            User? user = authRepository.Login(email, password);
            if (user != null)
            {
                System.Console.WriteLine("Successful login! Hello {0}", user.username);
                return user;
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input");
        }
        return null;

    }
    public void Register()
    {

        System.Console.WriteLine("Enter an Email:");
        string? email = Console.ReadLine();
        System.Console.WriteLine("Enter a Username:");
        string? username = Console.ReadLine();
        System.Console.WriteLine("Enter a Password:");
        string? password = Console.ReadLine();
        System.Console.WriteLine("Confirm Password:");
        string? confirm = Console.ReadLine();
        if (CheckRegistrationInfo(email, username, password, confirm))
        {
            if (authRepository.Register(username!, email!, password!))
            {
                System.Console.WriteLine($"Successful Registration! Proceed to login to access account");
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input");
        }


    }


    public bool CheckRegistrationInfo(string? email, string? username, string? password, string? confirm)
    {
        return !string.IsNullOrEmpty(username)
               && !string.IsNullOrEmpty(email)
               && !string.IsNullOrEmpty(password)
               && !string.IsNullOrEmpty(confirm)
               && password.CompareTo(confirm) == 0;
    }

}