using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class HikeRegion
    {
        public int ID { get; }
        public int CountryID { get; set; }
        public int HikeCount { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public HikeRegion() { }

        public HikeRegion(int idOfCountry, string regionName, string regionDescription)
        {
            CountryID = idOfCountry;
            Name = regionName;
            Description = regionDescription;
        }

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = "INSERT INTO region (name, idcountry, hikecount, description) VALUES (@name, @idCountry, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@idCountry", CountryID);
            command.Parameters.AddWithValue("@description", Description);
            return command;
        }
    }
}
