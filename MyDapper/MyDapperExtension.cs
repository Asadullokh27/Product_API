using MyDapper.Models;
using Npgsql;
using System.Xml.Linq;

namespace MyDapper
{
    public static class MyDapperExtension
    {
        public static List<T> Query<T>(this NpgsqlConnection connection, string sql)
        {
            connection.Open();

            List<T> list = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var entity = Activator.CreateInstance<T>();
                var properties = typeof(T).GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    properties[i].SetValue(entity, reader[i]);
                }

                list.Add(entity);
            }

            connection.Close();

            return list;
        }

        public static void Delete(this NpgsqlConnection connection, string sql)
        {
            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

        }

        public static void Create(this NpgsqlConnection connection, ProductDTO product)
        {
            connection.Open();

            string query = "INSERT INTO products (name, description, photopath) VALUES (@name, @description, @photopath)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@description", product.Description);
            cmd.Parameters.AddWithValue("@photopath", product.PhotoPath);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

    }
}


