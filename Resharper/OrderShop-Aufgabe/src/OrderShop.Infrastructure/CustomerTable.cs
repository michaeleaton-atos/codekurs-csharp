namespace OrderShop.Infrastructure;

/// <summary>Eine Zeile der Tabelle "customers".</summary>
public class CustomerRecord
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsActive { get; set; }
}

/// <summary>
/// Simulierte Kundentabelle. Die Daten sind statisch und werden
/// von allen Teilen der Anwendung gemeinsam benutzt.
/// </summary>
public static class CustomerTable
{
    public static List<CustomerRecord> Rows { get; } = new()
    {
        new() { Id = 1, Name = "Anna Muster",   Email = "anna@example.ch",  Phone = "+41 79 111 11 11", IsActive = true },
        new() { Id = 2, Name = "Beat Keller",   Email = "beat@example.ch",  Phone = "+41 79 222 22 22", IsActive = true },
        new() { Id = 3, Name = "Carla Rossi",   Email = "carla@example.ch", Phone = "+41 79 333 33 33", IsActive = false },
    };
}
