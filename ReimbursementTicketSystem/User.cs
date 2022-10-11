public enum UserPosition
{
    Employee, Manager
}

public class User
{
    public UserPosition userPosition;
    public List<Ticket> tickets;

    private string _email;
    private string _username;

    public string username { get => _username; }

    private string _password;

    public User(string email, string username, string password)
    {
        tickets = new List<Ticket>();
        this._email = email;
        this._password = password;
        this._username = username;
    }

    internal bool testPassword(string password)
    {
        return this._password.Equals(password);
    }

}