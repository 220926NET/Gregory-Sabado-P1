


namespace Models;

using System;
using System.Security.Cryptography;


public class User
{

    private int _id;
    private string _username;
    private string _password;

    public bool manager;
    public int id { get => _id; set => _id = value; }
    public string username { get => _username; set => _username = value; }
    public string password { get => _password; set => _password = value; }

    public User()
    {
        this._username = "";
        this._password = "";
    }

}