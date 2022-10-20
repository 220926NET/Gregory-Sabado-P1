using Microsoft.Data.SqlClient;

public class TicketRepository
{
    //id of ticket, and the ticket
    private SqlConnection conn;

    public TicketRepository()
    {
        conn = new SqlConnection("Server=tcp:gjs-revature.database.windows.net,1433;Initial Catalog=TRS_DB;Persist Security Info=False;User ID=gjs-admin;Password=secret@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    public int Submit(decimal amount, string desc, User owner)
    {
        int id = -1;
        conn.Open();
        string sql = @$"insert into tickets ([owner_id], [status], [amount], [desc])
                        output inserted.id values(@owner_id, @status, @amount, @desc)";
        SqlCommand cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@owner_id", owner.id);
        cmd.Parameters.AddWithValue("@status", (int)Status.Pending);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@desc", desc);

        id = (int)cmd.ExecuteScalar();

        cmd.Dispose();
        conn.Close();
        return id;
    }

    public Queue<Ticket> GetPendingTickets()
    {
        Queue<Ticket> tickets = new Queue<Ticket>();

        conn.Open();

        string sql = "SELECT * FROM tickets WHERE [status]=0";
        SqlCommand cmd = new SqlCommand(sql, conn);

        SqlDataReader dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            int owner_id = dataReader.GetInt32(1);
            Status status = (Status)dataReader.GetInt32(2);
            decimal amount = dataReader.GetDecimal(3);
            string desc = dataReader.GetString(4);
            Ticket ticket = new Ticket(id, amount, desc, owner_id, status);
            tickets.Enqueue(ticket);
        }


        cmd.Dispose();
        conn.Close();
        return tickets;
    }

    public void UpdateTickets(List<Ticket> tickets)
    {
        conn.Open();
        string sql = "update tickets set status=@status where id=@id";
        foreach (Ticket ticket in tickets)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", (int)ticket.status);
            cmd.Parameters.AddWithValue("@id", ticket.id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        conn.Close();
    }


    public List<Ticket> GetSubmittedTickets(User user)
    {
        List<Ticket> tickets = new List<Ticket>();

        conn.Open();

        string sql = "SELECT * FROM tickets WHERE [owner_id]=@owner_id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@owner_id", user.id);

        SqlDataReader dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            int owner_id = dataReader.GetInt32(1);
            Status status = (Status)dataReader.GetInt32(2);
            decimal amount = dataReader.GetDecimal(3);
            string desc = dataReader.GetString(4);
            Ticket ticket = new Ticket(id, amount, desc, owner_id, status);
            tickets.Add(ticket);
        }
        dataReader.Close();
        conn.Close();


        return tickets;

    }
}