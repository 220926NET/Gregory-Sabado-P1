//handles looking up stuff in database
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Models;

namespace DataAccess;

public interface IAuthRepository : IDisposable
{
    User GetUser(string username, string password);
    User GetUser(int id);
    int CreateUser(string username, string password);
    void DeleteUser(int id);
    void UpdateUser(User user);
}


public class AuthRepository : IAuthRepository
{
    private SqlConnection conn;

    public AuthRepository()
    {
        conn = SqlConnectionFactory.GetConnection();
    }


    //receives user credentials
    //returns validation of successful login
    public User GetUser(string username, string password)
    {
        User user = new();
        if (LookupUser(username))
        {
            string salt = GetUserSalt(username);
            if (TestPassword(username, password, salt))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM users WHERE username=@username";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["id"];
                        bool manager = (bool)dataReader["manager"];
                        salt = (string)dataReader["salt"];
                        user.id = id;
                        user.username = username;
                        user.password = password;
                        user.manager = manager;
                    }
                    cmd.Dispose();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();

                }
            }
        }
        return user;
    }

    private string GetUserSalt(string username)
    {
        string salt = "";
        try
        {
            conn.Open();
            string sql = "SELECT * FROM users WHERE username=@username";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                salt = (string)dataReader["salt"];
            }
            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();

        }
        return salt;
    }

    private bool LookupUser(string username)
    {
        int count = 0;
        try
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM users WHERE username=@username";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            count = (int)cmd.ExecuteScalar();
            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }

        return count > 0 ? true : false;

    }

    private bool TestPassword(string username, string password, string salt)
    {
        byte[] password_hash = GetHash(password + salt);
        int count = 0;
        try
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM users WHERE username=@username AND password_hash=@password_hash";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password_hash", password_hash);
            count = (int)cmd.ExecuteScalar();
            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }

        return count > 0 ? true : false;
    }


    public int CreateUser(string username, string password)
    {
        int id = -1;
        if (!LookupUser(username))
            try
            {
                conn.Open();
                string sql = "INSERT INTO users (username, password_hash, salt, manager) OUTPUT INSERTED.id VALUES(@username, @password_hash, @salt, 0)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                string salt = GetSalt();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password_hash", GetHash(password + salt));
                cmd.Parameters.AddWithValue("@salt", salt);
                id = (int)cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        return id;
    }

    private byte[] GetHash(string password)
    {
        using (HashAlgorithm algorithm = SHA512.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static string GetSalt()
    {
        Random random = new Random();

        // Maximum length of salt
        int max_length = 32;

        // Empty salt array
        byte[] salt = new byte[max_length];

        // Build the random bytes
        random.NextBytes(salt);

        // Return the string encoded salt
        return Convert.ToBase64String(salt);
    }

    public void DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }

    public User GetUser(int id)
    {
        User user = new();
        try
        {
            conn.Open();
            string sql = "SELECT * FROM users WHERE id=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                bool manager = (bool)dataReader["manager"];
                string salt = (string)dataReader["salt"];
                string username = (string)dataReader["username"];
                user.id = id;
                user.username = username;
                user.manager = manager;
            }
            cmd.Dispose();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
        return user;
    }
}
// EOC