using OrderShop.Core.Interfaces;

namespace OrderShop.Core.Notifications
{
    // Formatiert Benachrichtigungen als Markdown.
    // Trennt die Formatierung vom Versand, damit Kanal und Format unabhängig kombiniert werden könen.
    public class MarkdownFormatter : INotificationFormatter
    {
        public string Format(string subject, string body)
        {
            return $"## {subject}\n\n{body}\n\n_Ihr OrderShop-Team_";
        }
    }
}