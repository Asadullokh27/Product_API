using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();
        }

        public Product Add(Product product)
        {
            _products.Add(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return _products;
        }
    }
}
