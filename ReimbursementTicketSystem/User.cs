
public class User
{
    public bool manager;

    private int _id;
    public int id { get => _id; }
    private string _email;
    private string _username;

    public string username { get => _username; }


    private string _password;

    public User(int id, string email, string username, string password, bool manager)
    {
        this._id = id;
        this._email = email;
        this._password = password;
        this._username = username;
        this.manager = manager;
    }

    internal bool testPassword(string password)
    {
        return this._password.Equals(password);
    }

}