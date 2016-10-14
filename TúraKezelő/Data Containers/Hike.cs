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
    public class Hike
    {
        public int IDHike { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public int Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DateTime HikeDate { get; set; }
        public HikeType HikeType { get; set; }
        public List<int> CPList { get; set; }

        public Hike()
        {
            CPList = new List<int>();
        }

        public Hike(int hikeID)
        {
            IDHike = hikeID;
            CPList = new List<int>();
        }

        public string GetCPString()
        {
            string cpString = string.Empty;
            foreach (int item in CPList)
            {
                cpString += "." + item + ".";
            }
            return cpString;
        }

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = @"INSERT INTO hike (date, idregion, idcountry, type, description, cpstring) 
VALUES (@date, @idregion, @idcountry, @type, @description, @cpstring)";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@date", HikeDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@idregion", IDRegion);
            command.Parameters.AddWithValue("@idcountry", IDCountry);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
            command.Parameters.AddWithValue("@cpstring", GetCPString());
            return command;
        }

        public static void UpdatePositions(MySqlConnection connection)
        {            
            DataTable hikesTable = new DataTable();
            DataTable table = new DataTable();
            int position;
            DateTime date;
            string commandText = "SELECT idhike, date FROM hike WHERE position IS NULL AND type='túra';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
            {
                try
                {
                    adapter.Fill(hikesTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return;
                }
            }
            //MessageBox.Show(hikesTable.Rows.Count.ToString());
            foreach (DataRow row in hikesTable.Rows)
            {
                date = Convert.ToDateTime(row["date"]);
                commandText = "SELECT COUNT(*) AS count FROM hike WHERE date < '"+ date.ToString("yyyy-MM-dd") +"' AND type='túra';";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
                {
                    try
                    {
                        adapter.Fill(table);
                        int.TryParse((table.Rows[0]["count"]).ToString(), out position);
                        position++;
                        commandText = "UPDATE hike SET position=" + position + " WHERE idhike=" + row["idhike"] + " AND type='túra';";
                        //MessageBox.Show(commandText);
                        using (MySqlCommand command = new MySqlCommand(commandText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                        date = Convert.ToDateTime(row["date"]);
                        MovePositions(date, connection, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hiba");
                        return;
                    }
                }
            }
        }
         
        // Moves all positions by +1 or -1 after a certain date
        // +1 if upOrDown = true
        public static void MovePositions(DateTime date, MySqlConnection connection, bool upOrDown)
        {
            string commandText = "UPDATE hike SET position=position+1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";            
            if (!upOrDown)
                commandText = "UPDATE hike SET position=position-1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            string commandText = "UPDATE hike SET description=@description, type=@type, cpstring=@cpstring";
            if (HikeType != HikeType.túra)
                commandText += ", position = NULL";
            commandText += " WHERE idhike=@idhike;";
            //MessageBox.Show(commandText);
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
            command.Parameters.AddWithValue("@cpstring", GetCPString());
            command.Parameters.AddWithValue("@idhike", IDHike);
            return command;
        }

        public MySqlCommand DeleteCommand(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        
        
    }
}
