public class User
{
    private string _email;
    private string _username;

    public string username { get => _username; }

    private string _password;

    public User(string email, string username, string password)
    {
        this._email = email;
        this._password = password;
        this._username = username;
    }

    internal bool testPassword(string password)
    {
        return this._password.Equals(password);
    }

}