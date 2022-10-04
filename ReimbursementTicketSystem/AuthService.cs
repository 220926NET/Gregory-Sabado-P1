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
        if (!string.IsNullOrEmpty(email))
        {
            if (authRepository.lookupUser(email))
            {
                System.Console.WriteLine("Password:");
                string? password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password))
                {
                    if (authRepository.login(email, password))
                    {
                        System.Console.WriteLine("Successful login! Hello {0}", getCurrentUser().name);
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Email not found");
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input");
        }

    }
    public void register()
    {

    }

    public User getCurrentUser()
    {
        return authRepository.getCurrentUser();
    }

}