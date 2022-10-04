//handles looking up stuff in database
public class AuthRepository
{
    private Dictionary<string, User> database = new Dictionary<string, User>();
    private User? current_user;


    public AuthRepository()
    {
        string email = "gregjsabado@gmail.com";
        string password = "secret";
        string name = "Greg Sabado";
        User me = new User(email, password, name);
        database.Add(email, me);
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

    public void register()
    {

    }

    internal User getUser(string email)
    {
        return database[email];
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