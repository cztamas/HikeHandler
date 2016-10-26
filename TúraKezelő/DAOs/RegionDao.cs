using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using HikeHandler.Data_Containers;
using HikeHandler.Exceptions;

namespace HikeHandler.DAOs
{
    public class RegionDao
    {
        private MySqlConnection sqlConnection;

        public RegionDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        public DataTable SearchRegion(HikeRegionTemplate template)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = @"SELECT r.idregion AS id, r.name AS name, r.hikecount AS hikecount, 
r.description AS description, c.name AS countryname FROM region r, country c 
WHERE region.idcountry=country.idcountry AND region.name LIKE @name AND country.name LIKE @cname";
            if (template.HikeCount != null)
            {
                string countCondition = template.HikeCount.SqlSearchCondition("region.hikecount");
                if (countCondition != String.Empty)
                    commandText += (" AND " + countCondition);
            }
            if (template.IDcountry != null)
                commandText += (" AND country.idcountry=@idcountry");
            if (template.IDRegion != null)
                commandText += (" AND region.idregion=@idregion");
            commandText += " ORDER BY name ASC;";

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + template.Name + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@cname", "%" + template.CountryName + "%");
                if (template.IDcountry != null)
                    adapter.SelectCommand.Parameters.AddWithValue("@idcountry", template.IDcountry);
                if (template.IDRegion != null)
                    adapter.SelectCommand.Parameters.AddWithValue("@idregion", template.IDRegion);
                DataTable resultTable = new DataTable();
                try
                {
                    adapter.Fill(resultTable);
                    return resultTable;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Search, ErrorType.DBError, ex.Message);
                }
            }
        }
    }
}
