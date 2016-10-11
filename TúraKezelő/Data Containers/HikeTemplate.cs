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
        public int IDHike { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public IntPile Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DatePile HikeDate { get; set; }

        public HikeTemplate()
        { }

        public HikeTemplate(int hikeID)
        {
            IDHike = hikeID;
        }

        public MySqlCommand SearchCommand(MySqlConnection connection)
        {
            return null;
        }

        
    }
}
