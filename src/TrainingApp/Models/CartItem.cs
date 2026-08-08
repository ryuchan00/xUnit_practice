namespace TrainingApp.Models;

public record CartItem(string ProductName, decimal UnitPrice, int Quantity)
{
    public decimal Subtotal => UnitPrice * Quantity;
}
