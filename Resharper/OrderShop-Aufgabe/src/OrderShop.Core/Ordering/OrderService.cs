using OrderShop.Core.Customers;
using OrderShop.Core.Notifications;
using OrderShop.Infrastructure;

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
    private readonly PostgreSqlOrderRepository _repository = new();
    private readonly CustomerService _customers = new();
    private readonly Notification _notification = new EmailPlainTextNotification();

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

        var now = DateTime.Now;
        if (now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            discount += subtotal * 0.05m;
        }

        var total = Math.Round((subtotal - discount) * 20m, MidpointRounding.AwayFromZero) / 20m;
        discount = subtotal - total;

        var id = _repository.ExecuteInsert(
            "INSERT INTO orders (customer_id, subtotal, discount, total, created_at) " +
            "VALUES (@customer_id, @subtotal, @discount, @total, @created_at)",
            new Dictionary<string, object>
            {
                ["customer_id"] = customerId,
                ["subtotal"] = subtotal,
                ["discount"] = discount,
                ["total"] = total,
                ["created_at"] = now,
            });

        _notification.Send(
            customer,
            $"Bestellbestätigung #{id}",
            $"Vielen Dank für Ihre Bestellung über CHF {total:F2}.");

        return new Order(id, customerId, subtotal, discount, total, now);
    }

    public Order? GetOrder(int id)
    {
        var row = _repository.ExecuteSelectById("SELECT * FROM orders WHERE id = @id", id);
        if (row is null)
        {
            return null;
        }

        return new Order(
            (int)row["id"],
            (int)row["customer_id"],
            (decimal)row["subtotal"],
            (decimal)row["discount"],
            (decimal)row["total"],
            (DateTime)row["created_at"]);
    }
}
