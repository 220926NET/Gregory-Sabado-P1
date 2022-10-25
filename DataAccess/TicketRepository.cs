using Microsoft.Data.SqlClient;
using Models;

namespace DataAccess;

public interface ITicketRepository
{
    void UpdateTicket(int ticket_id, Status status);
    void DeleteTicket();

    List<Ticket> GetTickets(int author);
    List<Ticket> GetTickets(Status status);
    Ticket GetTicket(int id);
    int Submit(Ticket ticket);

}

public class TicketRepository : ITicketRepository
{
    //id of ticket, and the ticket
    private SqlConnection conn;

    public TicketRepository()
    {
        conn = SqlConnectionFactory.GetConnection();
    }

    public int Submit(Ticket ticket)
    {
        int id = -1;
        try
        {
            conn.Open();
            string sql = @$"insert into tickets ([author], [status], [amount], [desc], [type])
                        output inserted.id values(@author, @status, @amount, @desc, @type)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@author", ticket.author);
            cmd.Parameters.AddWithValue("@status", ticket.status.ToString());
            cmd.Parameters.AddWithValue("@amount", ticket.amount);
            cmd.Parameters.AddWithValue("@desc", ticket.desc);
            cmd.Parameters.AddWithValue("@type", ticket.type.ToString());

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


    public void UpdateTicket(int ticket_id, Status status)
    {
        try
        {
            conn.Open();
            string sql = "update tickets set status=@status where id=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status.ToString());
            cmd.Parameters.AddWithValue("@id", ticket_id);
            cmd.ExecuteNonQuery();
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



    public void DeleteTicket()
    {
        throw new NotImplementedException();
    }

    public Ticket GetTicket(int id)
    {
        Ticket ticket = new();
        try
        {
            conn.Open();

            string sql = "SELECT * FROM tickets WHERE [id]=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                int author = (int)dataReader["author"];
                Status status = (Status)Enum.Parse(typeof(Status), (string)dataReader["status"]);
                decimal amount = (decimal)dataReader["amount"];
                string desc = (string)dataReader["desc"];
                ReimbursementType type = (ReimbursementType)Enum.Parse(typeof(ReimbursementType), (string)dataReader["type"]);
                ticket = new Ticket
                {
                    id = id,
                    author = author,
                    status = status,
                    amount = amount,
                    desc = desc,
                    type = type
                };
            }
            dataReader.Close();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
        return ticket;
    }

    public List<Ticket> GetTickets(int author)
    {
        List<Ticket> tickets = new List<Ticket>();

        try
        {
            conn.Open();

            string sql = "SELECT * FROM tickets WHERE [author]=@author";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@author", author);

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                int id = (int)dataReader["id"];
                Status status = (Status)Enum.Parse(typeof(Status), (string)dataReader["status"]);
                decimal amount = (decimal)dataReader["amount"];
                string desc = (string)dataReader["desc"];
                ReimbursementType type = (ReimbursementType)Enum.Parse(typeof(ReimbursementType),
                                                                       (string)dataReader["type"]);

                Ticket ticket = new Ticket
                {
                    id = id,
                    author = author,
                    status = status,
                    amount = amount,
                    desc = desc,
                    type = type
                };
                tickets.Add(ticket);
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
        return tickets;
    }

    public List<Ticket> GetTickets(Status status)
    {
        List<Ticket> tickets = new List<Ticket>();

        try
        {
            conn.Open();

            string sql = "SELECT * FROM tickets WHERE [status]=@status";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status.ToString());

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                int id = (int)dataReader["id"];
                int author = (int)dataReader["author"];
                decimal amount = (decimal)dataReader["amount"];
                string desc = (string)dataReader["desc"];
                ReimbursementType type = (ReimbursementType)Enum.Parse(typeof(ReimbursementType),
                                                                       (string)dataReader["type"]);

                Ticket ticket = new Ticket
                {
                    id = id,
                    author = author,
                    status = status,
                    amount = amount,
                    desc = desc,
                    type = type
                };
                tickets.Add(ticket);
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
        return tickets;
    }
}