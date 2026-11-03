using OrderShop.Core.Customers;
using OrderShop.Core.Notifications;
using OrderShop.Core.Interfaces;
namespace OrderShop.Core.Ordering;

/// <summary>
/// Fachlogik für Bestellungen.
///
/// Geschäftsregeln:
///  1. Nur bestehende, aktive Kunden dürfen bestellen.
///  2. Eine Bestellung braucht mindestens eine Position.
///  3. Ab einem Warenwert von CHF 200.00 gibt es 10 % Rabatt.
///  4. Am Wochenende (Samstag/Sonntag) gibt es zusätzlich 5 % Rabatt (auf den Warenwert).
///  5. Der Endbetrag wird auf 5 Rappen gerundet.
///  6. Nach dem Speichern erhält der Kunde eine Bestellbestätigung.
/// </summary>
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly ICustomerReader _customers;
    private readonly Notification _notification;

    public OrderService(IOrderRepository repository, ICustomerReader customers, Notification notification,
        TimeProvider timeProvider)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _customers = customers ?? throw new ArgumentNullException(nameof(customers));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    private readonly TimeProvider _timeProvider;

    public Order PlaceOrder(int customerId, IReadOnlyList<OrderLine> lines)
    {
        var customer = _customers.GetById(customerId)
            ?? throw new InvalidOperationException($"Kunde {customerId} existiert nicht.");

        if (!customer.IsActive)
        {
            throw new InvalidOperationException($"Kunde {customerId} ist nicht aktiv.");
        }

        if (lines.Count == 0)
        {
            throw new ArgumentException("Eine Bestellung braucht mindestens eine Position.", nameof(lines));
        }

        var subtotal = lines.Sum(l => l.Quantity * l.UnitPrice);

        decimal discount = 0m;
        if (subtotal >= 200m)
        {
            discount += subtotal * 0.10m;
        }

        var now = _timeProvider.GetLocalNow().DateTime;
        if (now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            discount += subtotal * 0.05m;
        }

        var total = Math.Round((subtotal - discount) * 20m, MidpointRounding.AwayFromZero) / 20m;
        discount = subtotal - total;
        var orderToSave = new Order(
            0,
            customerId,
            subtotal,
            discount,
            total,
            now);

        var id = _repository.Save(orderToSave);

        _notification.Send(
            customer,
            $"Bestellbestätigung #{id}",
            $"Vielen Dank für Ihre Bestellung über CHF {total:F2}.");

        return new Order(id, customerId, subtotal, discount, total, now);
    }

    public Order? GetOrder(int id)
    {
        return _repository.GetById(id);
    }
}
