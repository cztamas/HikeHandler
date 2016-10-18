using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class HikeRegion
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public int HikeCount { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }

        public HikeRegion() { }

        public HikeRegion(int idOfCountry, string regionName, string regionDescription)
        {
            CountryID = idOfCountry;
            Name = regionName;
            Description = regionDescription;
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount, or -1 in case of an error.
        public static int UpdateHikeCount(int idRegion, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idregion=" + idRegion + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return -1;
                    commandText = "UPDATE region SET hikecount=@hikecount WHERE idregion=@idregion;";
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idregion", idRegion);
                        command.ExecuteNonQuery();
                        return count;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return -1;
                }
            }
        }

        // Returns the number of checkpoints corresponding to the given region, or -1 in case of an error.
        public static int CountCPs(int idRegion, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idregion=@idCregion;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idregion", idRegion);
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                        return -1;
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }
        }

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = @"INSERT INTO region (name, idcountry, hikecount, description) 
VALUES (@name, @idCountry, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@idCountry", CountryID);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }
        
        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            string commandText = "UPDATE region SET name=@name, description=@description WHERE idregion=@idregion;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@idregion", ID);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }

        public static bool IsDeletable(int idRegion, MySqlConnection connection)
        {
            if (UpdateHikeCount(idRegion, connection) != 0)
                return false;
            if (CountCPs(idRegion, connection) != 0)
                return false;
            return true;
        }
    }
}
