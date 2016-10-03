using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class Country
    {
        private int id;
        private string name;
        private int hikeCount;
        private string description;

        public Country(int countryID, int hikeNumber, string countryName, string countryDescription)
        {
            id = countryID;
            hikeCount = hikeNumber;
            name = countryName;
            description = countryDescription;
        }

        public Country(string countryName, string countryDescription)
        {
            name = countryName;
            description = countryDescription;
            hikeCount = 0;
        }
        
        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = "INSERT INTO country (NAME, HIKECOUNT, DESCRIPTION) VALUES (@name, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            return command;
        }

        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            string commandText = "UPDATE country SET NAME=@name, DESCRIPTION=@description WHERE IDCOUNTRY=@id;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@id", id);
            return command;
        }
    }
}
