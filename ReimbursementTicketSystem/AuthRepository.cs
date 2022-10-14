//handles looking up stuff in database



public class AuthRepository
{
    private SqlConnection conn = new SqlConnection("Server=tcp:gjs-revature.database.windows.net,1433;Initial Catalog=TRS_DB;Persist Security Info=False;User ID=gjs-admin;Password=secret@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    public AuthRepository()
    {
    }


    //receives user credentials
    //returns validation of successful login
    internal User? Login(string input, string password)
    {
        if (LookupUser(input))
        {
            if (TestPassword(input, password))
            {
                return GetUser(input);
            }
            else
            {
                //password incorrect
                System.Console.WriteLine("Password incorrect");
                return null;
            }
        }
        else
        {
            //email not found
            System.Console.WriteLine("Email not found");
            return null;
        }
    }

    internal bool Register(string username, string email, string password)
    {
        if (!LookupUser(email))
        {
            CreateUser(email, username, password);
            return true;
        }
        else
        {
            System.Console.WriteLine("Account for email already exists");
            return false;
        }

    }



    private User? GetUser(string input)
    {
        User? user = null;
        conn.Open();
        string sql = "SELECT * FROM users WHERE email=@email OR username=@username";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", input);
        cmd.Parameters.AddWithValue("@username", input);

        SqlDataReader dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            string username = dataReader.GetString(2);
            Boolean manager = dataReader.GetBoolean(3);
            string password = dataReader.GetString(4);
            if (manager)
                user = new User(id, input, username, password, true);
            else
                user = new User(id, input, username, password, false);

        }
        cmd.Dispose();
        conn.Close();
        return user;
    }

    private bool LookupUser(string input)
    {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE (email = @email OR username=@username)", conn);
        cmd.Parameters.AddWithValue("@email", input);
        cmd.Parameters.AddWithValue("@username", input);
        int UserExist = (int)cmd.ExecuteScalar();

        cmd.Dispose();
        conn.Close();

        return (UserExist > 0) ? true : false;
    }

    private void CreateUser(string email, string username, string password)
    {
        conn.Open();
        string sql = "insert into users (email, username, user_position, password_hash) values(@email, @username, 0, @password)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", GetHashString(password));

        cmd.ExecuteNonQuery();

        cmd.Dispose();
        conn.Close();
    }

    private bool TestPassword(string input, string password)
    {
        conn.Open();
        string sql = "SELECT COUNT(*) FROM users WHERE ((email = @email OR username=@username) and password_hash=@password_hash)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", input);
        cmd.Parameters.AddWithValue("@username", input);
        cmd.Parameters.AddWithValue("@password_hash", GetHashString(password));
        int UserExist = (int)cmd.ExecuteScalar();

        cmd.Dispose();
        conn.Close();

        return (UserExist > 0) ? true : false;
    }

    private byte[] GetHash(string password)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
    private string GetHashString(string password)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(password))
            sb.Append(b.ToString("X2"));
        return sb.ToString();
    }

}