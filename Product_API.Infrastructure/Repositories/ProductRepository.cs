using MyDapper;
using Npgsql;
using Product.Domain.Entities;

namespace Product_API.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = "Server = localhost; Port = 5432; Database = ProductAPI; Username = postgres; Password = sardor0618!";
        }

        public Product.Domain.Entities.ProductModel Add(Product.Domain.Entities.ProductModel product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO products (Id,Name, Description, PhotoPath) VALUES ({product.Id}, '{product.Name}','{product.Description}','{product.PhotoPath}') RETURNING *", connection);

                command.ExecuteNonQuery();

                return product;
            }
        }

        public List<Product.Domain.Entities.ProductModel> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var query = "Select * from Products";
                var products = connection.Query<ProductModel>(query);

                return products;
            }
        }
    }
}
