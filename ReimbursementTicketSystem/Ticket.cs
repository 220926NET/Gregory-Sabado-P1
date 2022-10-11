enum Status
{
    Pending, Approved, Denied
}

public class Ticket
{
    Status status;
    uint amount;
    uint id;

    string desc;

    User owner;


    public Ticket(uint id, uint amount, string desc, User owner)
    {
        this.id = id;
        this.amount = amount;
        this.desc = desc;
        this.owner = owner;
        status = Status.Pending;
    }

    public bool Approve()
    {
        if (this.status == Status.Pending)
        {
            this.status = Status.Approved;
            return true;

        }
        return false;
    }
    public bool Deny()
    {
        if (this.status == Status.Pending)
        {
            this.status = Status.Denied;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"ID: {this.id}\tAmount: {this.amount}\tDescription: {this.desc}\tStatus: {this.status}";
    }


}