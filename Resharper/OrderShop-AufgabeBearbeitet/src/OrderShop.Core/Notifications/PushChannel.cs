using OrderShop.Core.Customers;
using OrderShop.Core.Interfaces;

namespace OrderShop.Core.Notifications
{
    // Verschickt eine bereits formatierte Benachrichtigung als Push-Nachricht.
    // Ermöglicht Push unabhängig vom Nachrichtenformat zu verwenden.
    public class PushChannel : INotificationChannel
    {
        public void Send(Customer recipient, string subject, string content)
        {
            Console.WriteLine($"PUSH an {recipient.Email}: {content}");
        }
    }
}