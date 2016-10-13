using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        public HikeTemplate()
        { }

        public HikeTemplate(int hikeID)
        {
            IDHike = hikeID;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            string commandText = @"SELECT h.idhike, h.position, h.date, r.name AS 'regionname', c.name AS 'countryname', h.type 
FROM hike h, region r, country c ORDER BY h.position ASC
WHERE h.idcountry=c.idcountry AND h.idregion=c.idregion AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (IDHike != null)
                commandText += " AND cp.idcp=" + IDHike;
            if (IDRegion != null)
                commandText += " AND r.idregion=" + IDRegion;
            if (IDCountry != null)
                commandText += " AND c.idcountry=" + IDCountry;
            if (HikeDate != null)
                commandText += " AND " + HikeDate.SqlSearchCondition("h.date");
            if (Position != null)
                commandText += " AND " + Position.SqlSearchCondition("h.position");
            if (HikeType != null)
                commandText += " AND type = @type";
            commandText += ";";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@countryName", "%" + CountryName + "%");
            command.Parameters.AddWithValue("@regionName", "%" + RegionName + "%");
            if (HikeType != null)
                command.Parameters.AddWithValue("@type", HikeType.ToString());
            return command;
        }

        
    }
}
