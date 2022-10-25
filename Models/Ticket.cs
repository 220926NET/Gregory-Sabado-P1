

namespace Models;
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
    private ReimbursementType _type;

    private decimal _amount;
    private int _id;
    private string _desc;
    private int _author;

    public ReimbursementType type { get => _type; set => _type = value; }
    public Status status { get => _status; set => _status = value; }
    public decimal amount { get => _amount; set => _amount = value; }
    public int id { get => _id; set => _id = value; }
    public string desc { get => _desc; set => _desc = value; }
    public int author { get => _author; set => _author = value; }

    public Ticket()
    {
        this._desc = "";
    }

}