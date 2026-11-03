using OrderShop.Core.Interfaces;

namespace OrderShop.Core.Notifications;

// --------------------------------------------------------------------------
// Benachrichtigungen: eine Unterklasse pro Kombination aus Kanal und Format.
//
// Neue Anforderung vom Product Owner:
//   - zusätzlicher Kanal "Push"
//   - zusätzliches Format "HTML"
// --------------------------------------------------------------------------

public class Notification
{
    private readonly INotificationChannel _channel;
    private readonly INotificationFormatter _formatter;

    //objekte in variable speichern(Konstruktor Injection)
    public Notification(
        INotificationChannel channel,
        INotificationFormatter formatter)
    {
        _channel = channel;
        _formatter = formatter;
    }


    // Senden mit den jeweiligen objekten
    public void Send(Customer recipient, string subject, string body)
    {
        string content = _formatter.Format(subject, body);

        _channel.Send(recipient, subject, content);
    }
}





