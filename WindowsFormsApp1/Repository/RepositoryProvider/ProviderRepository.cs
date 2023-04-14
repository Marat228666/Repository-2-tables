using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Web;
using WindowsFormsApp1.models;
using WindowsFormsApp1.Repository.RepositoryPovider;


namespace WindowsFormsApp1.Repository.RepositoryProvider
{
    internal class ProviderRepository : IProviderRepository
    {
        private string ConnString { get; set; }
        public ProviderRepository(string host, string db, string user, string password)
        {
            ConnString = $"server={host};uid={user}; pwd={password};database={db}";
        }

        public List<provider> GetAll()
        {
            List<provider> table = new List<provider>();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"SELECT*FROM SHOP.provider;";
            try
            {
                conn.OpenAsync();
                MySqlDataReader reader;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    table.Add(new provider { id = reader.GetInt32(0), name = reader.GetString(1), second_name = reader.GetString(2), number = reader.GetInt32(3) });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");
                
            }
            conn.CloseAsync();
            return table;
        }

        public int insert(provider value)
        {
            int rows = 0;
            MySqlConnection conn=new MySqlConnection(ConnString);
            MySqlCommand com=conn.CreateCommand();
            com.CommandText = $"INSERT INTO SHOP.provider(name, second_name, number) VALUES('{value.name}','{value.second_name}', '{value.number}');";
            try
            {
                conn.OpenAsync();
                rows = com.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");
                
            }
            conn.CloseAsync();
            return rows;
        }

        public int update(int id, provider value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"UPDATE SHOP.provider SET name='{value.name}', second_name='{value.second_name}', number='{value.number}' WHERE id={id};";
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
