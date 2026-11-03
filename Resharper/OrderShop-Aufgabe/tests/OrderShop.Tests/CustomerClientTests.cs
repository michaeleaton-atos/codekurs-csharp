using OrderShop.Core;
using OrderShop.Core.Customers;

namespace OrderShop.Tests;

public class CustomerDetailViewTests
{
    [Fact]
    public void Render_ExistingCustomer_ShowsNameEmailAndStatus()
    {
        var fake = new FakeCustomerService(new Customer
        {
            Id = 7, Name = "Test Person", Email = "test@example.ch", IsActive = true,
        });
        var view = new CustomerDetailView(fake);

        var result = view.Render(7);

        Assert.Equal("Kunde #7: Test Person <test@example.ch> (aktiv)", result);
    }

    [Fact]
    public void Render_UnknownCustomer_ShowsNotFound()
    {
        var view = new CustomerDetailView(new FakeCustomerService());

        var result = view.Render(99);

        Assert.Equal("Kunde #99 nicht gefunden.", result);
    }
}

public class CustomerReportJobTests
{
    [Fact]
    public void Run_ReturnsExportedCsv()
    {
        var fake = new FakeCustomerService { CsvToReturn = "Id;Name\n1;Anna\n" };
        var job = new CustomerReportJob(fake);

        var csv = job.Run();

        Assert.Equal("Id;Name\n1;Anna\n", csv);
    }
}

/// <summary>
/// Handgeschriebener Testdouble für ICustomerService.
/// </summary>
internal class FakeCustomerService : ICustomerService
{
    private readonly List<Customer> _customers;

    public FakeCustomerService(params Customer[] customers)
    {
        _customers = customers.ToList();
    }

    public string CsvToReturn { get; set; } = "";

    public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

    public IReadOnlyList<Customer> Search(string term) => throw new NotImplementedException();

    public void Update(Customer customer) => throw new NotImplementedException();

    public void Delete(int id) => throw new NotImplementedException();

    public string ExportToCsv() => CsvToReturn;

    public void SendWelcomeMail(int customerId) => throw new NotImplementedException();
}
