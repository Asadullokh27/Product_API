using Product.Domain.Entities;

namespace Product_API.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        ProductModel Add(ProductModel product);
        List<ProductModel> GetAll();
    }
}
