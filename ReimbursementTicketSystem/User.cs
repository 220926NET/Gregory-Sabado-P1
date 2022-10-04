public class User
{
    private string _email;
    private string _name;

    public string name { get => _name; }

    private string _password;

    public User(string email, string name, string password)
    {
        this._email = email;
        this._password = password;
        this._name = name;
    }

    internal bool testPassword(string password)
    {
        return this._password.Equals(password);
    }

}