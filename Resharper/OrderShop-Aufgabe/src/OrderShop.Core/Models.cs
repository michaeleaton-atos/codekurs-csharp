namespace OrderShop.Core;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsActive { get; set; }
}

public record OrderLine(string ProductName, int Quantity, decimal UnitPrice);

public record Order(
    int Id,
    int CustomerId,
    decimal Subtotal,
    decimal Discount,
    decimal Total,
    DateTime CreatedAt);
