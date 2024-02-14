using Product.Domain.Entities;

namespace Product_API.Repositories
{
    public interface IProductRepository
    {
        ProductModel Add(ProductModel product);
        List<ProductModel> GetAll();
    }
}
