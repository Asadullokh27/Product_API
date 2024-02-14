using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public Product Add(Product product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO products (Id,Name, Description, PhotoPath) VALUES ({product.Id}, '{product.Name}','{product.Description}','{product.PhotoPath}') RETURNING *", connection);

                command.ExecuteNonQuery();

                return product;
            }
        }

        public List<Product> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("select * from Products", connection);

                var datareader = command.ExecuteReader();

                List<Product> products = new List<Product>();
                while (datareader.Read())
                {
                    var product = new Product()
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
