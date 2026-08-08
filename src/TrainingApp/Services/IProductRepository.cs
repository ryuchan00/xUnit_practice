using TrainingApp.Models;

namespace TrainingApp.Services;

public interface IProductRepository
{
    Product? GetById(int id);

    IReadOnlyList<Product> GetAll();

    Product Add(Product product);
}
