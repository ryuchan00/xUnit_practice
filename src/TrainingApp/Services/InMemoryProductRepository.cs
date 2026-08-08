using TrainingApp.Models;

namespace TrainingApp.Services;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = [];
    private int _nextId = 1;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<Product> GetAll() => _products;

    public Product Add(Product product)
    {
        var created = product with { Id = _nextId++ };
        _products.Add(created);
        return created;
    }
}
