using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.models;

namespace WindowsFormsApp1.Repository.RepositoryProduct
{
    internal class ProductRepository : IProductRepository
    {
        private string ConnString { get; set; }
        public ProductRepository(string host, string db, string user, string password)
        {
            ConnString = $"server={host};uid={user}; pwd={password};database={db}";
        }
        public List<product> GetAll()
        {
            List<product> table = new List<product>();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"SELECT*FROM SHOP.product;";
            try
            {
                conn.OpenAsync();
                MySqlDataReader reader;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    table.Add(new product { id = reader.GetInt32(0), name = reader.GetString(1), price=reader.GetInt32(2), weight = reader.GetInt32(3), provider_id=reader.GetInt32(4) });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");
                
            }
            conn.CloseAsync();
            return table;
        }

        public int insert(product value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"INSERT INTO SHOP.product(name, price, weight, provider_id) VALUES('{value.name}','{value.price}', '{value.weight}', '{value.provider_id}');";
            try
            {
                conn.OpenAsync();
                rows = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");
                
            }
            conn.CloseAsync();
            return rows;
        }

        public int update(int id, product value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"UPDATE SHOP.product SET name='{value.name}', price='{value.price}', weight='{value.weight}', provider_id='{value.provider_id}' WHERE id={   id};";
            try
            {
                conn.OpenAsync();
                rows = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");
                
            }
            conn.CloseAsync();
            return rows;
        }
    }
}
