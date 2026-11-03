using OrderShop.Core.Ordering;

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
}
