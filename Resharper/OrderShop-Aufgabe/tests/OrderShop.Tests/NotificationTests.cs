using OrderShop.Core;
using OrderShop.Core.Interfaces;
using OrderShop.Core.Notifications;

namespace OrderShop.Tests;

public class NotificationTests
{
    [Fact]
    public void Notification_SendsMessage()
    {
        var customer = new Customer
        {
            Id = 1,
            Name = "Max",
            IsActive = true
        };

        var channel = new FakeNotificationChannel();

        var notification = new Notification(
            channel,
            new PlainTextFormatter());

        notification.Send(
            customer,
            "Test",
            "Hallo");

        Assert.True(channel.WasSent);
    }

    private sealed class FakeNotificationChannel : INotificationChannel
    {
        public bool WasSent { get; private set; }

        public void Send(
            Customer recipient,
            string subject,
            string content)
        {
            // Es wird nichts wirklich verschickt.
            // Der Fake merkt sich nur, dass Send() aufgerufen wurde.
            WasSent = true;
        }
    }
}