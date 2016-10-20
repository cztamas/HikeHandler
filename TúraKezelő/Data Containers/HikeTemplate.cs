using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    public class HikeTemplate
    {
        public int? IDHike { get; set; }
        public int? IDCountry { get; set; }
        public int? IDRegion { get; set; }
        public IntPile Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DatePile HikeDate { get; set; }
        public HikeType? HikeType { get; set; }
        public List<int> CPList { get; set; }

        public HikeTemplate()
        {
            CPList = new List<int>();
        }

        public HikeTemplate(int hikeID)
        {
            IDHike = hikeID;
            CPList = new List<int>();
        }

        public string GetCPString()
        {
            string cpString = string.Empty;
            foreach (int item in CPList)
            {
                cpString += "." + item + ".";
            }
            return cpString;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection, bool anyCPOrder)
        {
            string commandText = @"SELECT h.idhike, h.position, h.date, h.idregion, r.name AS 'regionname', h.idcountry, c.name AS 'countryname', h.type, 
h.description, h.cpstring FROM hike h, region r, country c WHERE h.idcountry=c.idcountry AND h.idregion=r.idregion 
AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (IDHike != null)
                commandText += " AND h.idhike=" + IDHike;
            if (IDRegion != null)
                commandText += " AND r.idregion=" + IDRegion;
            if (IDCountry != null)
                commandText += " AND c.idcountry=" + IDCountry;
            if (HikeDate != null)
            {
                if (HikeDate.SqlSearchCondition("h.date") != string.Empty)
                    commandText += " AND " + HikeDate.SqlSearchCondition("h.date");
            }
            if (Position != null)
            {
                if (Position.SqlSearchCondition("h.position") != string.Empty)
                    commandText += " AND " + Position.SqlSearchCondition("h.position");
            }
            if (HikeType != null)
                commandText += " AND type = @type";
            if (anyCPOrder)
            {
                foreach (int item in CPList)
                {
                    commandText += " AND cpstring LIKE '%." + item.ToString() + ".%'";
                }
            }
            if (!anyCPOrder)
            {
                string condition = string.Empty;
                foreach (int item in CPList)
                {
                    condition += "%." + item + ".%";
                }
                commandText += " AND cpstring LIKE '" + condition + "'";
            }
            commandText += " ORDER BY h.date ASC;";
            //MessageBox.Show(commandText);
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@countryName", "%" + CountryName + "%");
            command.Parameters.AddWithValue("@regionName", "%" + RegionName + "%");
            if (HikeType != null)
                command.Parameters.AddWithValue("@type", HikeType.ToString());
            return command;
        }

        
    }
}
