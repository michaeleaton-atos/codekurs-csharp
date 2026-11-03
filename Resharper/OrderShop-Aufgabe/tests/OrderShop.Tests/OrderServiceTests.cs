namespace OrderShop.Tests;

/// <summary>
/// Diese Tests sollen die Geschäftsregeln des OrderService prüfen.
/// Im aktuellen Zustand lassen sie sich nicht sinnvoll schreiben.
///
/// Entferne nach dem Refactoring jeweils das "Skip" und implementiere den Test.
/// </summary>
public class OrderServiceTests
{
    private const string NotTestable =
        "Noch nicht testbar: OrderService erzeugt Datenbank, Kundenzugriff, Benachrichtigung und Uhrzeit selbst.";

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_Below200_NoDiscount_OnWeekday() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_From200_Gives10PercentDiscount_OnWeekday() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_OnSaturday_GivesAdditional5PercentDiscount() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_RoundsTotalTo5Rappen() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_UnknownCustomer_Throws() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_InactiveCustomer_Throws() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_WithoutLines_Throws() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_SavesOrder_AndItCanBeLoadedAgain() { }

    [Fact(Skip = NotTestable)]
    public void PlaceOrder_SendsConfirmationToCustomer() { }
}
