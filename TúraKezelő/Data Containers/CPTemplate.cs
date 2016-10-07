using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class CPTemplate
    {
        public int CPID { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType TypeOfCP { get; set; }
        public string Description { get; set; }
        public IntPile HikeCount { get; set; }

        public CPTemplate()
        {
            CPID = -1;
            IDCountry = -1;
            IDRegion = -1;
        }

        public CPTemplate(int id)
        {
            CPID = id;
            IDCountry = -1;
            IDRegion = -1;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = @"SELECT cp.idcp, cp.name, cp.type, cp.hikecount, r.name, country.name, cp.description
FROM cp, region r, country c WHERE cp.idregion=r.idregion AND cp.idcountry=c.idcountry
AND cp.name LIKE @name AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (CPID != -1)
                commandText += " AND cp.idcp=" + CPID;
            if (IDRegion != -1)
                commandText += " AND r.idregion=" + IDRegion;
            if (IDCountry != -1)
                commandText += " AND c.idcountry=" + IDCountry;
            if (HikeCount != null)
                commandText += HikeCount.SqlSearchCondition("cp.hikecount");
            if (TypeOfCP != 0)
                commandText += " AND cp.type=@type";
            commandText += ";";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@countryName", CountryName);
            command.Parameters.AddWithValue("@regionName", RegionName);
            if (TypeOfCP != 0)
                command.Parameters.AddWithValue("@type", TypeOfCP.ToString());
            return command;
        }        
    }
}
