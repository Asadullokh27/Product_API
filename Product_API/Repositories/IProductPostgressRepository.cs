using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        public Product Add(Product product)
        {
            return new Product();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
