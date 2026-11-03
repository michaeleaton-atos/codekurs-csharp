using OrderShop.Core;
using OrderShop.Core.Interfaces;

namespace OrderShop.Infrastructure
{
    // Verschickt eine bereits formatierte Benachrichtigung per E-Mail.
    // Trennt den Versand vom Nachrichtenformat.
    // Liegt in Infrastructure, da hier konkret auf den SmtpMailClient zugegriffen wird.
    public class EmailChannel : INotificationChannel
    {
        public void Send(Customer recipient, string subject, string content)
        {
            var smtp = new SmtpMailClient();
            smtp.Send(recipient.Email, subject, content);
        }
    }
}
