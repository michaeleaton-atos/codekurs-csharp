namespace OrderShop.Infrastructure;

/// <summary>
/// Simuliert den Zugriff auf eine PostgreSQL-Datenbank.
/// (Es gibt keine echte Datenbank – die "Tabelle" liegt im Speicher.)
/// </summary>
public class PostgreSqlOrderRepository
{
    private readonly Dictionary<int, Dictionary<string, object>> _ordersTable = new();
    private int _nextId = 1;

    public string ConnectionString { get; }

    public PostgreSqlOrderRepository()
    {
        ConnectionString = Environment.GetEnvironmentVariable("ORDERSHOP_PG_CONNECTION")
            ?? throw new InvalidOperationException(
                "Keine Datenbankverbindung: Umgebungsvariable ORDERSHOP_PG_CONNECTION fehlt.");

        Console.WriteLine($"[PostgreSQL] Verbindung zu '{ConnectionString}' geöffnet.");
    }

    /// <summary>Führt ein INSERT aus und gibt die generierte ID zurück.</summary>
    public int ExecuteInsert(string sql, Dictionary<string, object> parameters)
    {
        Console.WriteLine($"[PostgreSQL] {sql}");

        var id = _nextId++;
        var row = new Dictionary<string, object>(parameters) { ["id"] = id };
        _ordersTable[id] = row;
        return id;
    }

    /// <summary>Führt ein SELECT ... WHERE id = @id aus. Gibt null zurück, wenn nichts gefunden wurde.</summary>
    public Dictionary<string, object>? ExecuteSelectById(string sql, int id)
    {
        Console.WriteLine($"[PostgreSQL] {sql}  -- @id = {id}");
        return _ordersTable.TryGetValue(id, out var row) ? row : null;
    }
}
