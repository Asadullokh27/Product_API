using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductPostgressRepository(IConfiguration configuration)
        {
            _connectionString = "Server=localhost;Port=5432;Database=exam;Username=postgres;Password=2712;";
        }

        public Product Add(Product product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var insertedProduct = connection.QueryFirstOrDefault<Product>(
                    "INSERT INTO products (Name, Description, PhotoPath) VALUES (@Name, @Description, @PhotoPath) RETURNING *",
                    new { product.Name, product.Description, product.PhotoPath });

                return insertedProduct;
            }
        }

        public List<Product> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var products = connection.Query<Product>("SELECT * FROM products").AsList();

                return products;
            }
        }
    }
}
