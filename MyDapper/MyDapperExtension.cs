using Npgsql;

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
    }
}
