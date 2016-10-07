using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    class HikeRegionTemplate
    {
        private int id = 0;
        private int countryID = 0;
        private string countryName;
        private IntPile hikeCount;
        private string name;

        public HikeRegionTemplate(int regionID)
        {
            id = regionID;
        }

        public HikeRegionTemplate(string nameOfCountry, string regionName, IntPile hikeNumber)
        {
            countryName = nameOfCountry;
            name = regionName;
            hikeCount = hikeNumber;
        }

        public HikeRegionTemplate(string nameOfCountry, string regionName, string hikeNumber)
        {
            countryName = nameOfCountry;
            name = regionName;
            hikeCount = hikeNumber.ToIntPile();
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = @"SELECT region.idregion AS id, region.name AS name, region.hikecount AS hikecount, 
region.description AS description, country.name AS countryname FROM region INNER JOIN country 
ON region.idcountry=country.idcountry WHERE region.name LIKE @name AND country.name LIKE @cname"; 
            if (hikeCount!=null)
            {
                string countCondition = hikeCount.SqlSearchCondition("region.hikecount");
                if (countCondition != String.Empty)
                    commandText += (" AND " + countCondition);
            }            
            if (countryID != 0)
                commandText += (" AND country.idcountry=@idcountry");
            if (id != 0)
                commandText += (" AND region.idregion=@idregion");
            commandText += ";";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", "%" + name + "%");
            command.Parameters.AddWithValue("@cname", "%" + countryName + "%");
            if (countryID != 0)
                command.Parameters.AddWithValue("@idcountry", countryID);
            if (id != 0)
                command.Parameters.AddWithValue("@idregion", id);
            //MessageBox.Show(commandText);
            return command;
        }

    }
}
