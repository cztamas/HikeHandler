using HikeHandler.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TúraKezelő.DAOs
{
    public class DBManager
    {
        private CountryDao countryDao;
        private RegionDao regionDao;
        private CPDao cpDao;
        private HikeDao hikeDao;

        public DBManager(MySqlConnection connection)
        {
            countryDao = new CountryDao(connection);
            regionDao = new RegionDao(connection);
            cpDao = new CPDao(connection);
            hikeDao = new HikeDao(connection);
        }
    }
}
