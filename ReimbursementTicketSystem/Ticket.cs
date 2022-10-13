public enum Status
{
    Pending, Approved, Denied
}

public class Ticket
{
    private Status _status;
    public Status status { get => _status; }
    private decimal amount;
    private int _id;
    public int id { get => _id; }

    private string desc;

    private int _owner_id;
    public int owner_id { get => _owner_id; }


    public Ticket(int id, decimal amount, string desc, int owner_id, Status status)
    {
        this._id = id;
        this.amount = amount;
        this.desc = desc;
        this._owner_id = owner_id;
        this._status = status;
    }

    internal bool Approve()
    {
        if (this.status == Status.Pending)
        {
            System.Console.WriteLine("Approved!");
            this._status = Status.Approved;
            return true;
        }
        return false;
    }
    internal bool Deny()
    {
        if (this.status == Status.Pending)
        {
            System.Console.WriteLine("Denied!");
            this._status = Status.Denied;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"ID: {this._id}\tAmount: {this.amount}\tDescription: {this.desc}\tStatus: {this.status}";
    }


}