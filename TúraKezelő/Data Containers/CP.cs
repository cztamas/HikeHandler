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
    public class CP
    {
        public int CPID { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType? TypeOfCP { get; set; }
        public string Description { get; set; }
        public int HikeCount { get; set; }

        public CP() { }

        public CP(int idCP)
        {
            CPID = idCP;
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount, or -1 in case of an error.
        public static int UpdateHikeCount(int idCP, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE cpstring LIKE '%." + idCP + ".%';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return -1;
                    commandText = "UPDATE cp SET hikecount=@hikecount WHERE idcp=@idcp;";
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idcp", idCP);
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

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = @"INSERT INTO cp (name, idcountry, idregion, type, hikecount, description) 
VALUES (@name, @idcountry, @idregion, @type, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@idcountry", IDCountry);
            command.Parameters.AddWithValue("@idregion", IDRegion);
            string typeString = string.Empty;
            if (TypeOfCP != null)
                typeString = TypeOfCP.ToString();
            command.Parameters.AddWithValue("@type", typeString);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }

        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            string commandText = "UPDATE cp SET name=@name, type=@type, description=@description WHERE idcp=@idcp;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            string typeString = string.Empty;
            if (TypeOfCP != null)
                typeString = TypeOfCP.ToString();
            command.Parameters.AddWithValue("@type", typeString);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@idcp", CPID);
            return command;
        }

        public bool IsDuplicateName(MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) FROM cp WHERE name=@name;";
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
        }

        public static bool IsDeletable(int idCP, MySqlConnection connection)
        {
            if (UpdateHikeCount(idCP, connection) != 0)
                return false;
            return true;
        }

        public static bool DeleteCP(int idCP, MySqlConnection connection)
        {
            string message = "Biztosan törli?";
            string caption = "CheckPoint törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            string commandText = "DELETE FROM cp WHERE idcp=@idcp";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcp", idCP);
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
        }
    }
}
