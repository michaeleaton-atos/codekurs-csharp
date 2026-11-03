using OrderShop.Core.Interfaces;

namespace OrderShop.Core.Notifications
{
    // Formatiert Benachrichtigungen als HTML.
    // Ermöglicht HTML unabhängig vom Versandkanal zu verwenden.
    public class HtmlFormatter : INotificationFormatter
    {
        public string Format(string subject, string body)
        {
            return $"<h2>{subject}</h2><p>{body}</p><p><i>Ihr OrderShop-Team</i></p>";
        }
    }
}