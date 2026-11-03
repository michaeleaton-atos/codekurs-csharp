namespace OrderShop.Infrastructure;

/// <summary>Simulierter SMTP-Client. Schreibt nur auf die Konsole.</summary>
public class SmtpMailClient
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"[SMTP] An: {to} | Betreff: {subject}");
        Console.WriteLine(body);
        Console.WriteLine();
    }
}

/// <summary>Simuliertes SMS-Gateway. Schreibt nur auf die Konsole.</summary>
public class SmsGatewayClient
{
    public const int MaxLength = 160;

    public void Send(string phoneNumber, string text)
    {
        if (text.Length > MaxLength)
        {
            text = text[..(MaxLength - 3)] + "...";
        }

        Console.WriteLine($"[SMS] An: {phoneNumber}");
        Console.WriteLine(text);
        Console.WriteLine();
    }
}
