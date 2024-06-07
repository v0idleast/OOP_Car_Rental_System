public class Person
{
    public string Name { get; set; }
    public string Passport { get; set; }
    public decimal RentDays { get; set; }

    public Person(string name, decimal rentdays, string passport)
    {
        Name = name;
        RentDays = rentdays;
        Passport = passport;
    }
    public override string ToString()
    {
        return $"{Name}, {Passport} - {RentDays} days";
    }
}

public interface IRentable
{
    decimal RentDays { get; set; }
    int RentCost { get; set; }
    void CalculateCost();
}

public sealed class Client : Person, IRentable
{
    public int ClientId { get; set; }
    public int RentCost { get; set; }

    public Client(string name, decimal rentdays, string passport, int clientId, int rentCost) : base(name, rentdays, passport)
    {
        ClientId = clientId;
        RentDays = rentdays;
        RentCost = rentCost;
    }

    public void CalculateCost()
    {
        RentCost = (int)(RentCost * RentDays);
    }
}
