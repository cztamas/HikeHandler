using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    public class Country
    {
        public int ID { get; }
        public string Name { get; set; }
        public int HikeCount { get; }
        public string Description { get; set; }

        public Country(int countryID, int hikeNumber, string countryName, string countryDescription)
        {
            ID = countryID;
            HikeCount = hikeNumber;
            Name = countryName;
            Description = countryDescription;
        }

        public Country(string countryName, string countryDescription)
        {
            Name = countryName;
            Description = countryDescription;
            HikeCount = 0;
        }

        public Country(int countryID)
        {
            ID = countryID;
        }

        public static bool UpdateHikeCount(int idCountry, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idcountry=" + idCountry + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return false;                    
                    commandText = "UPDATE country SET hikecount=@hikecount WHERE idcountry=@idcountry;";
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idcountry", idCountry);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return false;
                }
            }
        }
        
        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = "INSERT INTO country (NAME, HIKECOUNT, DESCRIPTION) VALUES (@name, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }

        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            string commandText = "UPDATE country SET NAME=@name, DESCRIPTION=@description WHERE IDCOUNTRY=@id;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@id", ID);
            return command;
        }

        public MySqlCommand RefreshCommand(MySqlConnection connection)
        {
            string commandText = "SELECT name, description, hikecount FROM country WHERE IDCOUNTRY=@id;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", ID);
            return command;
        }
    }
}
