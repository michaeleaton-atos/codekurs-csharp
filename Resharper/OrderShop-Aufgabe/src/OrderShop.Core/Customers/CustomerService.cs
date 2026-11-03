using System.Text;
using OrderShop.Core.Notifications;
using OrderShop.Infrastructure;

namespace OrderShop.Core.Customers;

/// <summary>
/// Alles, was man mit Kunden machen kann – an einem Ort.
/// </summary>
public interface ICustomerService
{
    Customer? GetById(int id);
    IReadOnlyList<Customer> Search(string term);
    void Update(Customer customer);
    void Delete(int id);
    string ExportToCsv();
    void SendWelcomeMail(int customerId);
}

public class CustomerService : ICustomerService
{
    public Customer? GetById(int id)
    {
        var record = CustomerTable.Rows.FirstOrDefault(r => r.Id == id);
        return record is null ? null : Map(record);
    }

    public IReadOnlyList<Customer> Search(string term)
    {
        return CustomerTable.Rows
            .Where(r => r.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
            .Select(Map)
            .ToList();
    }

    public void Update(Customer customer)
    {
        var record = CustomerTable.Rows.FirstOrDefault(r => r.Id == customer.Id)
            ?? throw new InvalidOperationException($"Kunde {customer.Id} existiert nicht.");

        record.Name = customer.Name;
        record.Email = customer.Email;
        record.Phone = customer.Phone;
        record.IsActive = customer.IsActive;
    }

    public void Delete(int id)
    {
        CustomerTable.Rows.RemoveAll(r => r.Id == id);
    }

    public string ExportToCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id;Name;Email;Aktiv");
        foreach (var r in CustomerTable.Rows.OrderBy(r => r.Id))
        {
            sb.AppendLine($"{r.Id};{r.Name};{r.Email};{(r.IsActive ? "ja" : "nein")}");
        }
        return sb.ToString();
    }

    public void SendWelcomeMail(int customerId)
    {
        var customer = GetById(customerId)
            ?? throw new InvalidOperationException($"Kunde {customerId} existiert nicht.");

        var notification = new EmailPlainTextNotification();
        notification.Send(customer, "Willkommen bei OrderShop",
            $"Hallo {customer.Name}, schön, dass Sie da sind!");
    }

    private static Customer Map(CustomerRecord r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Email = r.Email,
        Phone = r.Phone,
        IsActive = r.IsActive,
    };
}
