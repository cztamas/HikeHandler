using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.DAOs
{
    public class HikeDao
    {
        private MySqlConnection sqlConnection;

        public HikeDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }
    }
}
