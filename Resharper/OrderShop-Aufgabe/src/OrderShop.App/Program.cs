using OrderShop.Core;
using OrderShop.Core.Customers;
using OrderShop.Core.Ordering;

// Simuliert die Konfiguration der Produktionsumgebung.
Environment.SetEnvironmentVariable("ORDERSHOP_PG_CONNECTION", "Host=db.ordershop.local;Database=shop");

Console.WriteLine("=== OrderShop ===\n");

var orderService = new OrderService();
var order = orderService.PlaceOrder(1, new List<OrderLine>
{
    new("Tastatur", 1, 129.00m),
    new("Maus", 2, 49.90m),
});

Console.WriteLine($"Bestellung #{order.Id}: Warenwert CHF {order.Subtotal:F2}, " +
                  $"Rabatt CHF {order.Discount:F2}, Total CHF {order.Total:F2}\n");

var customerService = new CustomerService();
customerService.SendWelcomeMail(2);

var detailView = new CustomerDetailView(customerService);
Console.WriteLine(detailView.Render(1));
Console.WriteLine(detailView.Render(3));
Console.WriteLine();

var reportJob = new CustomerReportJob(customerService);
Console.WriteLine("Kundenreport (CSV):");
Console.WriteLine(reportJob.Run());
