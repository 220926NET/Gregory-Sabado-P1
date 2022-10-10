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
        User me = new User(email, password, username);
        database.Add(username, me);
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
            System.Console.WriteLine("Email not found in database");
            return false;
        }
    }

    internal User getCurrentUser()
    {
        return current_user;
    }

    public bool register(string username, string email, string password)
    {
        if (!lookupUser(username))
        {
            User newUser = new User(email, username, password);
            database.Add(username, newUser);
            return true;
        }
        else
        {
            System.Console.WriteLine("Username already exists");
            return false;
        }

    }

    internal User getUser(string username)
    {
        return database[username];
    }

    internal bool lookupUser(string email)
    {
        return database.ContainsKey(email);
    }

    internal void createUser()
    {

    }

    internal bool testPassword(string email, string password)
    {
        return database[email].testPassword(password);
    }
}