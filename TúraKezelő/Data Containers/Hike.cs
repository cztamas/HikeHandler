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

        public Hike()
        { }

        public Hike(int hikeID)
        {
            IDHike = hikeID;
        }

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = @"INSERT INTO hike (date, idregion, idcountry, type, description) 
VALUES (@date, @idregion, @idcountry, @type, @description)";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@date", HikeDate.Date.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@idregion", IDRegion);
            command.Parameters.AddWithValue("@idcountry", IDCountry);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
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
                //MessageBox.Show(commandText);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connection))
                {
                    try
                    {
                        adapter.Fill(table);
                        //MessageBox.Show(table.Rows[0]["count"].ToString());
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
            string commandText = "UPDATE hike SET position=position+1 WHERE date > @date;";            
            if (!upOrDown)
                commandText = "UPDATE hike SET position=position-1 WHERE date > @date AND type='túra' AND position NOT NULL;";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@date", date.Date);
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
            string commandText = "UPDATE hike SET description=@description, type=@type WHERE idhike=@idhike";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
            command.Parameters.AddWithValue("@idhike", IDHike);
            return command;
        }

        public MySqlCommand DeleteCommand(MySqlConnection connection)
        {
            return null;
        }

        public static MySqlCommand RemoveFromPositionListCommand(int idHike, MySqlConnection connection)
        {
            return null;
        }
        
    }
}
