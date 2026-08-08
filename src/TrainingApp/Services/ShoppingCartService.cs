using TrainingApp.Models;

namespace TrainingApp.Services;

public class ShoppingCartService
{
    private readonly List<CartItem> _items = [];

    public IReadOnlyList<CartItem> Items => _items;

    public decimal TotalAmount => _items.Sum(i => i.Subtotal);

    public int TotalItemCount => _items.Sum(i => i.Quantity);

    public void AddItem(string productName, decimal unitPrice, int quantity)
    {
        _items.Add(new CartItem(productName, unitPrice, quantity));
    }

    public void RemoveItem(string productName)
    {
        _items.RemoveAll(i => i.ProductName == productName);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
