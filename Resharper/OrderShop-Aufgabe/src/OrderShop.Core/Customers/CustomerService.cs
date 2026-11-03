using System.Text;
using OrderShop.Core.Interfaces;
using OrderShop.Core.Notifications;

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
    private readonly ICustomerRepository _repository;
    private readonly Notification _notification;

    public CustomerService(
        ICustomerRepository repository,
        Notification notification)
    {
        _repository = repository;
        _notification = notification;
    }

    public Customer? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public IReadOnlyList<Customer> Search(string term)
    {
        return _repository.Search(term);
    }

    public void Update(Customer customer)
    {
        _repository.Update(customer);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public string ExportToCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id;Name;Email;Aktiv");
        foreach (var customer in _repository.GetAll())
        {
            sb.AppendLine($"{customer.Id};{customer.Name};{customer.Email};{(customer.IsActive ? "ja" : "nein")}");
        }
        return sb.ToString();
    }

    public void SendWelcomeMail(int customerId)
    {
        var customer = GetById(customerId)
                       ?? throw new InvalidOperationException(
                           $"Kunde {customerId} existiert nicht.");

        _notification.Send(
            customer,
            "Willkommen bei OrderShop",
            $"Hallo {customer.Name}, schön, dass Sie da sind!");
    }
}
