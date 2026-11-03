using OrderShop.Core;
using OrderShop.Core.Customers;
using OrderShop.Core.Interfaces;
using OrderShop.Core.Notifications;
using OrderShop.Core.Ordering;
using OrderShop.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// Simuliert die Konfiguration der Produktionsumgebung.
Environment.SetEnvironmentVariable("ORDERSHOP_PG_CONNECTION", "Host=db.ordershop.local;Database=shop");

Console.WriteLine("=== OrderShop ===\n");
var services = new ServiceCollection();// Di Container
// Transient: Repository wird bei Bedarf neu erstellt.
services.AddTransient<PostgreSqlOrderRepository>();

// Transient: adapter enthält keinen langlebigen Zustand.
services.AddTransient<IOrderRepository, PostgreSqlOrderRepositoryAdapter>();

// Transient: CustomerService wird bei Bedarf neu erstellt.
services.AddTransient<ICustomerRepository, CustomerRepository>();
services.AddTransient<CustomerService>();

// Transient: adapter delegiert nur an den CustomerService.
services.AddTransient<ICustomerReader, CustomerReaderAdapter>();

// Transient: Channel wird für die jeweilige Benachrichtigung erstelt.
services.AddTransient<INotificationChannel, EmailChannel>();

// Transient: Formater ist zustandslos und kann neu erstellt werden.
services.AddTransient<INotificationFormatter, PlainTextFormatter>();

// Transient: Notification wird für den jeweiligen Vorgang zusamengesetzt.
services.AddTransient<Notification>();

// Singleton: Eine gemeinsame Systemuhr reicht für die gesamte Anwendung.
services.AddSingleton(TimeProvider.System);

// Transient: OrderService verarbeitet einzelne vorgänge und speichert keinen langlebigen Zustand.
services.AddTransient<OrderService>();

var provider = services.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateScopes = true, //prüfung ob lebensdauern zusammenpassen
    ValidateOnBuild = true //prüfe die registrierungen sofort beim Erstellen des Containers.
});
var orderService = provider.GetRequiredService<OrderService>();


var order = orderService.PlaceOrder(1, new List<OrderLine>
{
    new("Tastatur", 1, 129.00m),
    new("Maus", 2, 49.90m),
});

Console.WriteLine($"Bestellung #{order.Id}: Warenwert CHF {order.Subtotal:F2}, " +
                  $"Rabatt CHF {order.Discount:F2}, Total CHF {order.Total:F2}\n");

var customerService = provider.GetRequiredService<CustomerService>();
customerService.SendWelcomeMail(2);

var detailView = new CustomerDetailView(customerService);
Console.WriteLine(detailView.Render(1));
Console.WriteLine(detailView.Render(3));
Console.WriteLine();

var reportJob = new CustomerReportJob(customerService);
Console.WriteLine("Kundenreport (CSV):");
Console.WriteLine(reportJob.Run());


class CustomerReaderAdapter : ICustomerReader
{
    private readonly CustomerService _inner;

    public CustomerReaderAdapter(CustomerService inner)
    {
        _inner = inner;
    }
    
    public Customer? GetById(int id) => _inner.GetById(id);
}


class PostgreSqlOrderRepositoryAdapter : IOrderRepository
{
    private readonly PostgreSqlOrderRepository _inner;

    public PostgreSqlOrderRepositoryAdapter(PostgreSqlOrderRepository inner)
    {
        _inner = inner;
    }

    public int Save(Order order)
    {
        var parameters = new Dictionary<string, object>
        {
            ["customerId"] = order.CustomerId,
            ["subtotal"] = order.Subtotal,
            ["discount"] = order.Discount,
            ["total"] = order.Total,
            ["createdAt"] = order.CreatedAt,
        };

        return _inner.ExecuteInsert(
            "INSERT INTO orders (customer_id, subtotal, discount, total, created_at) VALUES (@customerId, @subtotal, @discount, @total, @createdAt)",
            parameters);
    }

    public Order? GetById(int id)
    {
        var row = _inner.ExecuteSelectById("SELECT * FROM orders", id);
        if (row is null)
        {
            return null;
        }

        return new Order(
            id,
            (int)row["customerId"],
            (decimal)row["subtotal"],
            (decimal)row["discount"],
            (decimal)row["total"],
            (DateTime)row["createdAt"]);
    }
}
