using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.Data_Containers
{
    public class Hike
    {
        public int IDHike { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public int Position { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public DateTime HikeDate { get; set; }

        public Hike()
        { }

        public Hike(int hikeID)
        {
            IDHike = hikeID;
        }

        public MySqlCommand SaveCommand(MySqlConnection connection)
        {
            return null;
        }

        public MySqlCommand UpdateCommand(MySqlConnection connection)
        {
            return null;
        }

        public MySqlCommand DeleteCommand(MySqlConnection connection)
        {
            return null;
        }
    }
}
