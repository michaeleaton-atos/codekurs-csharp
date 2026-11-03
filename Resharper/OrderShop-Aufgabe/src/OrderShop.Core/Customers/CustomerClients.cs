namespace OrderShop.Core.Customers;

/// <summary>Zeigt die Detailansicht eines Kunden an (z.B. in einer UI).</summary>
public class CustomerDetailView
{
    private readonly ICustomerService _customers;

    public CustomerDetailView(ICustomerService customers)
    {
        _customers = customers;
    }

    public string Render(int customerId)
    {
        var customer = _customers.GetById(customerId);
        if (customer is null)
        {
            return $"Kunde #{customerId} nicht gefunden.";
        }

        var status = customer.IsActive ? "aktiv" : "inaktiv";
        return $"Kunde #{customer.Id}: {customer.Name} <{customer.Email}> ({status})";
    }
}

/// <summary>Nächtlicher Job, der eine Kundenliste für das Controlling erstellt.</summary>
public class CustomerReportJob
{
    private readonly ICustomerService _customers;

    public CustomerReportJob(ICustomerService customers)
    {
        _customers = customers;
    }

    public string Run()
    {
        return _customers.ExportToCsv();
    }
}
