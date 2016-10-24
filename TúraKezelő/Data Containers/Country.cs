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
        public int ID { get; set; }
        public string Name { get; set; }
        public int HikeCount { get; set; }
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

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount, or -1 in case of an error.
        /*public static int UpdateHikeCount(int idCountry, MySqlConnection connection)
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
                        return -1;                    
                    commandText = "UPDATE country SET hikecount=@hikecount WHERE idcountry=@idcountry;";
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idcountry", idCountry);
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
        }*/
        
        // Returns the number of regions corresponding to the given country, or -1 in case of an error.
        /*public static int CountRegions(int idCountry, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM region WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {                
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
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
        }*/

        // Returns the number of checkpoints corresponding to the given country, or -1 in case of an error.
        /*public static int CountCPs(int idCountry, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
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
        }*/

        // Returns the MySql command to save the item to DB.
        /*public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = "INSERT INTO country (NAME, HIKECOUNT, DESCRIPTION) VALUES (@name, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }*/

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

        /*public bool IsDuplicateName(MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) FROM country WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@name", Name);
                try
                {
                    int count;
                    if (!int.TryParse(command.ExecuteScalar().ToString(), out count))
                        return true;
                    if (count == 0)
                        return false;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return true;
                }
            }
        }*/

        // Checks whether the given country can be deleted.
        // Returns false in case of an error.
        // Deletable only if no region, CP or hike belongs to it.
        /*public static bool IsDeletable(MySqlConnection connection, int idCountry)
        {
            if (UpdateHikeCount(idCountry, connection) != 0)
                return false;
            if (CountRegions(idCountry, connection) != 0)
                return false;
            if (CountCPs(idCountry, connection) != 0)
                return false;
            return true;
        }*/

        /*public static bool DeleteCountry(int idCountry, MySqlConnection connection)
        {
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            string commandText = "DELETE FROM country WHERE idcountry=@idcountry";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Törölve.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return false;
                }
            }
        }*/
    }
}
