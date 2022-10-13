//handles looking up stuff in database



public class AuthRepository
{
    private User? current_user;
    private SqlConnection conn = new SqlConnection("Server=tcp:gjs-revature.database.windows.net,1433;Initial Catalog=TRS_DB;Persist Security Info=False;User ID=gjs-admin;Password=secret@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    public AuthRepository()
    {
    }


    //receives user credentials
    //returns validation of successful login
    public bool login(string email, string password)
    {
        if (lookupUser(email))
        {
            if (testPassword(email, password))
            {
                getUser(email, out current_user);
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

    internal User? getCurrentUser()
    {
        return current_user;
    }


    private void getUser(string email, out User? user)
    {
        user = null;
        conn.Open();
        string sql = "SELECT * FROM users WHERE email=@email";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", email);

        SqlDataReader dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            string username = dataReader.GetString(2);
            Boolean manager = dataReader.GetBoolean(3);
            string password = dataReader.GetString(4);
            if (manager)
                user = new User(id, email, username, password, true);
            else
                user = new User(id, email, username, password, false);

        }

        conn.Close();
    }

    private bool lookupUser(string email)
    {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE (email = @email)", conn);
        cmd.Parameters.AddWithValue("@email", email);
        int UserExist = (int)cmd.ExecuteScalar();
        conn.Close();

        return (UserExist > 0) ? true : false;
    }

    private void createUser(string email, string username, string password)
    {
        conn.Open();
        string sql = "insert into users (email, username, user_position, password_hash) values(@email, @username, 0, @password)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", GetHashString(password));

        cmd.ExecuteNonQuery();

        conn.Close();
    }

    private bool testPassword(string email, string password)
    {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE (email = @email and password_hash=@password_hash)", conn);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password_hash", GetHashString(password));
        int UserExist = (int)cmd.ExecuteScalar();
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