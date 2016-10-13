using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    public class CPTemplate
    {
        public int? CPID { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType? TypeOfCP { get; set; }
        public string Description { get; set; }
        public IntPile HikeCount { get; set; }

        public CPTemplate()
        { }

        public CPTemplate(int id)
        {
            CPID = id;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = @"SELECT cp.idcp, cp.name, cp.type, cp.hikecount, r.name, c.name, cp.description
FROM cp, region r, country c WHERE cp.idregion=r.idregion AND cp.idcountry=c.idcountry
AND cp.name LIKE @name AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (CPID != null)
                commandText += " AND cp.idcp=" + CPID;
            if (IDRegion != null)
                commandText += " AND r.idregion=" + IDRegion;
            if (IDCountry != null)
                commandText += " AND c.idcountry=" + IDCountry;
            if (HikeCount != null)
            {
                if (HikeCount.Count() > 0) 
                commandText += " AND " + HikeCount.SqlSearchCondition("cp.hikecount");
            }
            if (TypeOfCP != null)
                commandText += " AND cp.type=@type";
            commandText += ";";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", "%" + Name + "%");
            command.Parameters.AddWithValue("@countryName", "%" + CountryName + "%");
            command.Parameters.AddWithValue("@regionName", "%" + RegionName + "%");
            if (TypeOfCP != null)
                command.Parameters.AddWithValue("@type", TypeOfCP.ToString());
            //MessageBox.Show(commandText);
            return command;
        }        
    }
}
