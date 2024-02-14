using Npgsql;
using Product.Domain.Entities;

namespace Product_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
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
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("select * from Products", connection);

                var datareader = command.ExecuteReader();

                List<Product.Domain.Entities.ProductModel> products = new List<ProductModel>();
                while (datareader.Read())
                {
                    var product = new ProductModel()
                    {
                        Id = Convert.ToInt32(datareader[0]),
                        Name = Convert.ToString(datareader[1]),
                        Description = Convert.ToString(datareader[2]),
                        PhotoPath = Convert.ToString(datareader[3]),
                    };

                    products.Add(product);
                }

                return products;
            }
        }
    }
}
