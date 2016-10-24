using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HikeHandler.DAOs
{
    public class RegionDao
    {
        private MySqlConnection sqlConnection;

        public RegionDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }
    }
}
