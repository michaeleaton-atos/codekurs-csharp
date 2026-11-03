using OrderShop.Core.Ordering;
using Microsoft.Extensions.DependencyInjection;
using OrderShop.Core.Customers;
using OrderShop.Core.Interfaces;
using OrderShop.Core.Notifications;
using OrderShop.Infrastructure;

namespace OrderShop.Tests;

/// <summary>
/// Prüft die Richtung der Compile-Time-Abhängigkeit zwischen den Projekten.
/// Dieser Test ist zu Beginn ROT. Er muss nach deinem Refactoring GRÜN sein.
/// </summary>
public class ArchitectureTests
{
    [Fact]
    public void Core_DoesNotReference_Infrastructure()
    {
        var coreAssembly = typeof(OrderService).Assembly;

        var referenced = coreAssembly.GetReferencedAssemblies().Select(a => a.Name);

        Assert.DoesNotContain("OrderShop.Infrastructure", referenced);
    }

    [Fact]
    public void DiConfiguration_CanBuildProvider()
    {
        var services = new ServiceCollection();

        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<CustomerService>();

        services.AddTransient<INotificationChannel, EmailChannel>();
        services.AddTransient<INotificationFormatter, PlainTextFormatter>();
        services.AddTransient<Notification>();

        services.AddSingleton(TimeProvider.System);

        var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });

        Assert.NotNull(provider);
    }
}
