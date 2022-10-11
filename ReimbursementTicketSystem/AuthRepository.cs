//handles looking up stuff in database
public class AuthRepository
{
    private Dictionary<string, User> database = new Dictionary<string, User>();
    private User? current_user;


    public AuthRepository()
    {
        string email = "gregjsabado@gmail.com";
        string password = "secret";
        string username = "gsabado";
        createUser(email, username, password);
    }

    //receives user credentials
    //returns validation of successful login
    public bool login(string email, string password)
    {
        if (lookupUser(email))
        {
            if (testPassword(email, password))
            {
                current_user = getUser(email);
                return true;
            }
            else
            {
                //password incorrect
                System.Console.WriteLine("Password incorrect");
                return false;
            }
        }
        else
        {
            //email not found
            System.Console.WriteLine("Email not found");
            return false;
        }
    }

    public bool register(string username, string email, string password)
    {
        if (!lookupUser(email))
        {
            createUser(email, username, password);
            return true;
        }
        else
        {
            System.Console.WriteLine("Account for email already exists");
            return false;
        }

    }

    internal User getCurrentUser()
    {
        return current_user;
    }


    internal User getUser(string email)
    {
        return database[email];
    }

    internal bool lookupUser(string email)
    {
        return database.ContainsKey(email);
    }

    internal void createUser(string email, string username, string password)
    {
        User newUser = new User(email, username, password);
        database.Add(email, newUser);
    }

    internal bool testPassword(string email, string password)
    {
        return database[email].testPassword(password);
    }
}