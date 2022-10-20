public enum Status
{
    Pending, Approved, Denied
}

public enum ReimbursementType
{
    Travel,
    Lodging,
    Food,
    Other
}

public class Ticket
{
    private Status _status;
    public Status status { get => _status; set => _status = value; }

    private ReimbursementType reimbursementType = ReimbursementType.Other;
    private decimal amount;
    private int _id;
    public int id { get => _id; }

    private string desc;

    private int _owner_id;
    public int owner_id { get => _owner_id; }


    public Ticket(int id, decimal amount, string desc, int owner_id, Status status = Status.Pending, ReimbursementType reimbursementType = ReimbursementType.Other)
    {
        this._id = id;
        this.amount = amount;
        this.desc = desc;
        this._owner_id = owner_id;
        this._status = status;
    }

    public override string ToString()
    {
        return $"ID: {this._id}\tAmount: {this.amount.ToString("C")}\tType: {this.reimbursementType}\tDescription: {this.desc}\tStatus: {this.status}";
    }


}