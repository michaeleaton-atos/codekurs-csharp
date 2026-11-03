using OrderShop.Infrastructure;

namespace OrderShop.Core.Notifications;

// --------------------------------------------------------------------------
// Benachrichtigungen: eine Unterklasse pro Kombination aus Kanal und Format.
//
// Neue Anforderung vom Product Owner:
//   - zusätzlicher Kanal "Push"
//   - zusätzliches Format "HTML"
// --------------------------------------------------------------------------

public abstract class Notification
{
    public abstract void Send(Customer recipient, string subject, string body);
}

public class EmailPlainTextNotification : Notification
{
    public override void Send(Customer recipient, string subject, string body)
    {
        var text = $"{subject.ToUpperInvariant()}\n\n{body}\n\n-- \nIhr OrderShop-Team";
        var client = new SmtpMailClient();
        client.Send(recipient.Email, subject, text);
    }
}

public class EmailMarkdownNotification : Notification
{
    public override void Send(Customer recipient, string subject, string body)
    {
        var text = $"# {subject}\n\n{body}\n\n_Ihr OrderShop-Team_";
        var client = new SmtpMailClient();
        client.Send(recipient.Email, subject, text);
    }
}

public class SmsPlainTextNotification : Notification
{
    public override void Send(Customer recipient, string subject, string body)
    {
        var text = $"{subject.ToUpperInvariant()}\n\n{body}\n\n-- \nIhr OrderShop-Team";
        var client = new SmsGatewayClient();
        client.Send(recipient.Phone, text);
    }
}

public class SmsMarkdownNotification : Notification
{
    public override void Send(Customer recipient, string subject, string body)
    {
        var text = $"# {subject}\n\n{body}\n\n_Ihr OrderShop-Team_";
        var client = new SmsGatewayClient();
        client.Send(recipient.Phone, text);
    }
}
