﻿using System;
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
    }
}
