using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HikeHandler.DAOs;

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
        public string CPString { get; set; }

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

        public MySqlCommand UpdateCommand(MySqlConnection connection, bool dateChanged)
        {
            string commandText = "UPDATE hike SET date=@date, description=@description, type=@type, cpstring=@cpstring";
            if (HikeType != HikeType.túra || dateChanged)
                commandText += ", position = NULL";
            commandText += " WHERE idhike=@idhike;";
            //MessageBox.Show(commandText);
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@date", HikeDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
            command.Parameters.AddWithValue("@cpstring", GetCPString());
            command.Parameters.AddWithValue("@idhike", IDHike);
            return command;
        }
        
        public static bool DeleteHike(Hike hikeData, MySqlConnection connection)
        {
            string message = "Biztosan törli?";
            string caption = "Túra törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            string commandText = "DELETE FROM hike WHERE idhike=@idhike";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idhike", hikeData.IDHike);
                    command.ExecuteNonQuery();
                    MovePositions(hikeData.HikeDate, connection, false);
                    foreach (int item in hikeData.CPList)
                    {
                        CP.UpdateHikeCount(item, connection);
                    }
                    HikeRegion.UpdateHikeCount(hikeData.IDRegion, connection);

                    CountryDao countryDao = new CountryDao(connection);
                    countryDao.UpdateHikeCount(hikeData.IDCountry);
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
