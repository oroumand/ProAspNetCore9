namespace DependencyInjection.WebUI.Services;

public class Customer
{

    public string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public CustomerType CustomerType { get; set; }
    private Customer(CustomerType customerType)
    {
        CustomerType = customerType;
    }

    public static Customer GetGoldeCustomer()
    {
        return new Customer(CustomerType.Gold);
    }

    public static Customer GetSivlerCustomer()
    {
        return new Customer(CustomerType.Silver);
    }
}
