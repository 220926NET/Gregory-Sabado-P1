using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class SqlConnectionFactory
    {
        private const string _connectionString = $"Server=tcp:gjs-revature.database.windows.net,1433;Initial Catalog=TRS_DB;" +
            $"Persist Security Info=False;User ID=gjs-admin;Password=secret@123;MultipleActiveResultSets=False;Encrypt=True;" +
            $"TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
