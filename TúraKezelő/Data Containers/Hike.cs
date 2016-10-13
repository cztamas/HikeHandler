using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            command.Parameters.AddWithValue("@date", HikeDate.ToString());
            command.Parameters.AddWithValue("@idregion", IDRegion);
            command.Parameters.AddWithValue("@idcountry", IDCountry);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@type", HikeType.ToString());
            return command;
        }

        public static MySqlCommand FindPositionCommand(int idHike, MySqlConnection connection)
        {
            return null;
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
