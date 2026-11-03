using OrderShop.Core;
using OrderShop.Core.Notifications;

// Die Tests lesen die Konsolenausgabe mit und dürfen deshalb nicht parallel laufen.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace OrderShop.Tests;

public class NotificationTests
{
    private static readonly Customer Anna = new()
    {
        Id = 1, Name = "Anna", Email = "anna@example.ch", Phone = "+41 79 111 11 11", IsActive = true,
    };

    [Fact]
    public void EmailPlainText_WritesSubjectInUpperCase()
    {
        var output = CaptureConsole(() =>
            new EmailPlainTextNotification().Send(Anna, "Hallo", "Text"));

        Assert.Contains("[SMTP] An: anna@example.ch", output);
        Assert.Contains("HALLO", output);
    }

    [Fact]
    public void SmsMarkdown_UsesMarkdownHeading()
    {
        var output = CaptureConsole(() =>
            new SmsMarkdownNotification().Send(Anna, "Hallo", "Text"));

        Assert.Contains("[SMS] An: +41 79 111 11 11", output);
        Assert.Contains("# Hallo", output);
    }

    private static string CaptureConsole(Action action)
    {
        var original = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(original);
        }
        return writer.ToString();
    }
}
