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

        public static bool UpdateHikeCount(int idCP, MySqlConnection connection)
        {
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE cpstring LIKE '%." + idCP + ".%';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count = (int)table.Rows[0]["count"];
                    commandText = "UPDATE region SET hikecount=@hikecount WHERE idcp=@idcp;";
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idregion", idCP);
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


    }
}
