using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    public class HikeRegionTemplate
    {
        public int? IDRegion { get; set; }
        public int? IDcountry { get; set; }
        public string CountryName { get; set; }
        public IntPile HikeCount { get; set; }
        public string Name { get; set; }

        public HikeRegionTemplate() { }

        public HikeRegionTemplate(int regionID)
        {
            IDRegion = regionID;
        }

        public HikeRegionTemplate(string nameOfCountry, string regionName, IntPile hikeNumber)
        {
            CountryName = nameOfCountry;
            Name = regionName;
            HikeCount = hikeNumber;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = @"SELECT region.idregion AS id, region.name AS name, region.hikecount AS hikecount, 
region.description AS description, country.name AS countryname FROM region INNER JOIN country 
ON region.idcountry=country.idcountry WHERE region.name LIKE @name AND country.name LIKE @cname"; 
            if (HikeCount!=null)
            {
                string countCondition = HikeCount.SqlSearchCondition("region.hikecount");
                if (countCondition != String.Empty)
                    commandText += (" AND " + countCondition);
            }            
            if (IDcountry != null)
                commandText += (" AND country.idcountry=@idcountry");
            if (IDRegion != null)
                commandText += (" AND region.idregion=@idregion");
            commandText += " ORDER BY name ASC;";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", "%" + Name + "%");
            command.Parameters.AddWithValue("@cname", "%" + CountryName + "%");
            if (IDcountry != null)
                command.Parameters.AddWithValue("@idcountry", IDcountry);
            if (IDRegion != null)
                command.Parameters.AddWithValue("@idregion", IDRegion);
            //MessageBox.Show(commandText);
            return command;
        }

    }
}
