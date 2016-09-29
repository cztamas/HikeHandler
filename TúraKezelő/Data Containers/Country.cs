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
        public Country(int iD, int hC, string n, string d)
        {
            id = iD;
            hikeCount = hC;
            name = n;
            description = d;
        }

        public Country(string n, string d)
        {
            name = n;
            description = d;
            hikeCount = 0;
        }

        private int id;
        private string name;
        private int hikeCount;
        private string description;

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            string commandText = "INSERT INTO country (NAME, HIKECOUNT, DESCRIPTION) VALUES (@name, 0, @description);";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            return command;
        }
    }
}
