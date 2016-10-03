using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class CountryTemplate
    {
        private string name;
        private IntPile hikeCount;

        public CountryTemplate()
        {
            hikeCount = new IntPile();
            name = String.Empty;
        }

        public CountryTemplate(string countryName, IntPile hikePile)
        {            
            hikeCount = hikePile;
            name = countryName;
        }

        public CountryTemplate(string countryName, string hikePileString)
        {
            name = countryName;
            hikeCount = hikePileString.ToIntPile();
        }
        
        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = "SELECT countryid, name, hikecount FROM country WHERE name LIKE @name;";
            string countCondition = hikeCount.SqlSearchCondition("hikeCount");
            if (countCondition != String.Empty)
                commandText += ("AND " + countCondition);
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", "%" + name + "%");
            return command;
        }
        
    }
}
