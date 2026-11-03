using OrderShop.Core.Customers;
using OrderShop.Core.Interfaces;
using OrderShop.Core.Notifications;
using OrderShop.Core.Ordering;
using OrderShop.Core;

namespace OrderShop.Tests;

/// <summary>
/// Diese Tests sollen die Geschäftsregeln des OrderService prüfen.
/// Im aktuellen Zustand lassen sie sich nicht sinnvoll schreiben.
///
/// Entferne nach dem Refactoring jeweils das "Skip" und implementiere den Test.
/// </summary>
public class OrderServiceTests
{
    private const string NotTestable =
        "Noch nicht testbar: OrderService erzeugt Datenbank, Kundenzugriff, Benachrichtigung und Uhrzeit selbst.";
    [Fact]
    public void PlaceOrder_Below200_NoDiscount_OnWeekday()
    {
        var customer = new Customer
        {
            Id = 1,
            Name = "Max",
            Email = "max@test.ch",
            IsActive = true
        };
        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        var order = service.PlaceOrder(
            1,
            new List<OrderLine>
            {
                new("Produkt", 1, 100m)
            });

        Assert.Equal(0m, order.Discount);
        Assert.Equal(100m, order.Total);
    }



    [Fact]
    public void PlaceOrder_From200_Gives10PercentDiscount_OnWeekday()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(new FakeNotificationChannel(), new PlainTextFormatter()),
            new FixedTimeProvider(new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        var order = service.PlaceOrder(
            1,
            new List<OrderLine> { new("Produkt", 1, 200m) });

        Assert.Equal(20m, order.Discount);
        Assert.Equal(180m, order.Total);
    }

    [Fact]
    public void PlaceOrder_OnSaturday_GivesAdditional5PercentDiscount()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 7, 12, 0, 0, TimeSpan.Zero)));

        var order = service.PlaceOrder(
            1,
            new List<OrderLine>
            {
                new("Produkt", 1, 200m)
            });

        Assert.Equal(30m, order.Discount);
        Assert.Equal(170m, order.Total);
    }
    [Fact]
    public void PlaceOrder_RoundsTotalTo5Rappen()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        var order = service.PlaceOrder(
            1,
            new List<OrderLine>
            {
                new("Produkt", 1, 10.03m)
            });

        Assert.Equal(10.05m, order.Total);
    }

    [Fact]
    public void PlaceOrder_UnknownCustomer_Throws()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        Assert.Throws<InvalidOperationException>(() =>
            service.PlaceOrder(
                999,
                new List<OrderLine>
                {
                    new("Produkt", 1, 100m)
                }));
    }

    [Fact]
    public void PlaceOrder_InactiveCustomer_Throws()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = false
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        Assert.Throws<InvalidOperationException>(() =>
            service.PlaceOrder(
                1,
                new List<OrderLine>
                {
                    new("Produkt", 1, 100m)
                }));
    }

    [Fact]
    public void PlaceOrder_WithoutLines_Throws()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        Assert.Throws<ArgumentException>(() =>
            service.PlaceOrder(
                1,
                new List<OrderLine>()));
    }

    [Fact]
    public void PlaceOrder_SavesOrder_AndItCanBeLoadedAgain()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var repository = new FakeOrderRepository();

        var service = new OrderService(
            repository,
            new FakeCustomerReader(customer),
            new Notification(
                new FakeNotificationChannel(),
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        var order = service.PlaceOrder(
            1,
            new List<OrderLine>
            {
                new("Produkt", 1, 100m)
            });

        var loadedOrder = service.GetOrder(order.Id);

        Assert.NotNull(loadedOrder);
        Assert.Equal(order.Id, loadedOrder.Id);
        Assert.Equal(order.Total, loadedOrder.Total);
    }

    [Fact]
    public void PlaceOrder_SendsConfirmationToCustomer()
    {
        var customer = new Customer
        {
            Id = 1,
            IsActive = true
        };

        var channel = new FakeNotificationChannel();

        var service = new OrderService(
            new FakeOrderRepository(),
            new FakeCustomerReader(customer),
            new Notification(
                channel,
                new PlainTextFormatter()),
            new FixedTimeProvider(
                new DateTimeOffset(2026, 11, 2, 12, 0, 0, TimeSpan.Zero)));

        service.PlaceOrder(
            1,
            new List<OrderLine>
            {
                new("Produkt", 1, 100m)
            });

        Assert.True(channel.WasSent);
    }

    private sealed class FakeCustomerReader : ICustomerReader
    {
        private readonly Customer _customer;

        public FakeCustomerReader(Customer customer)
        {
            _customer = customer;
        }

        public Customer? GetById(int id)
        {
            return id == _customer.Id ? _customer : null;
        }
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        private Order? _order;

        public int Save(Order order)
        {
            _order = new Order(
                1,
                order.CustomerId,
                order.Subtotal,
                order.Discount,
                order.Total,
                order.CreatedAt);

            return 1;
        }

        public Order? GetById(int id)
        {
            return id == 1 ? _order : null;
        }
    }

    private sealed class FakeNotificationChannel : INotificationChannel
    {
        public bool WasSent { get; private set; }

        public void Send(
            Customer recipient,
            string subject,
            string content)
        {
            WasSent = true;
        }
    }

    private sealed class FixedTimeProvider : TimeProvider
    {
        private readonly DateTimeOffset _now;

        public FixedTimeProvider(DateTimeOffset now)
        {
            _now = now;
        }

        public override DateTimeOffset GetUtcNow()
        {
            return _now;
        }

        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}
