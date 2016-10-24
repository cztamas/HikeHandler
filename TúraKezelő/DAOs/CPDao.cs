using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.DAOs
{
    public class CPDao
    {
        private MySqlConnection sqlConnection;

        public CPDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }
    }
}
