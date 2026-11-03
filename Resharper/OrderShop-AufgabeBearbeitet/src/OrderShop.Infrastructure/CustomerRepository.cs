using OrderShop.Core;
using OrderShop.Core.Interfaces;

namespace OrderShop.Infrastructure;

/// <summary>
/// Konkrete Implementierung von <see cref="ICustomerRepository"/>.
/// Kapselt den Zugriff auf die simulierte Kundentabelle (<see cref="CustomerTable"/>)
/// und übersetzt zwischen dem technischen <see cref="CustomerRecord"/> und dem
/// fachlichen <see cref="Customer"/>-Modell aus Core.
/// </summary>
public class CustomerRepository : ICustomerRepository
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

    public IReadOnlyList<Customer> GetAll()
    {
        return CustomerTable.Rows
            .OrderBy(r => r.Id)
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

    private static Customer Map(CustomerRecord r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Email = r.Email,
        Phone = r.Phone,
        IsActive = r.IsActive,
    };
}
