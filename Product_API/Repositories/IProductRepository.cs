using Product_API.Models;

namespace Product_API.Repositories
{
    public interface IProductRepository
    {
        Product Add(Product product);
        List<Product> GetAll();
    }
}
