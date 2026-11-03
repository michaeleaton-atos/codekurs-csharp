using OrderShop.Core;
using OrderShop.Core.Interfaces;

namespace OrderShop.Infrastructure
{
    // Verschickt eine bereits formatierte Benachrichtigung per SMS.
    // Trennt den Versand vom Nachrichtenformat.
    // Liegt in Infrastructure, da hier konkret auf den SmsGatewayClient zugegriffen wird.
    public class SmsChannel : INotificationChannel
    {
        public void Send(Customer recipient, string subject, string content)
        {
            var sms = new SmsGatewayClient();
            sms.Send(recipient.Phone, content);
        }
    }
}
