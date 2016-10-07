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
    }
}
