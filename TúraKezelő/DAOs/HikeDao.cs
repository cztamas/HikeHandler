using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HikeHandler.Data_Containers;
using HikeHandler.Exceptions;

namespace HikeHandler.DAOs
{
    public class HikeDao
    {
        private MySqlConnection sqlConnection;

        public HikeDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        public DataTable SearchHike(HikeTemplate template, bool anyCPOrder)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = @"SELECT h.idhike, h.position, h.date, h.idregion, r.name AS 'regionname', h.idcountry, c.name AS 'countryname', h.type, 
h.description, h.cpstring FROM hike h, region r, country c WHERE h.idcountry=c.idcountry AND h.idregion=r.idregion 
AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (template.IDHike != null)
                commandText += " AND h.idhike=" + template.IDHike;
            if (template.IDRegion != null)
                commandText += " AND r.idregion=" + template.IDRegion;
            if (template.IDCountry != null)
                commandText += " AND c.idcountry=" + template.IDCountry;
            if (template.HikeDate != null)
            {
                if (template.HikeDate.SqlSearchCondition("h.date") != string.Empty)
                    commandText += " AND " + template.HikeDate.SqlSearchCondition("h.date");
            }
            if (template.Position != null)
            {
                if (template.Position.SqlSearchCondition("h.position") != string.Empty)
                    commandText += " AND " + template.Position.SqlSearchCondition("h.position");
            }
            if (template.HikeType != null)
                commandText += " AND type = @type";
            if (anyCPOrder)
            {
                foreach (int item in template.CPList)
                {
                    commandText += " AND cpstring LIKE '%." + item.ToString() + ".%'";
                }
            }
            if (!anyCPOrder)
            {
                string condition = string.Empty;
                foreach (int item in template.CPList)
                {
                    condition += "%." + item + ".%";
                }
                commandText += " AND cpstring LIKE '" + condition + "'";
            }
            commandText += " ORDER BY h.date ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@countryName", "%" + template.CountryName + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@regionName", "%" + template.RegionName + "%");
                if (template.HikeType != null)
                    adapter.SelectCommand.Parameters.AddWithValue("@type", template.HikeType.ToString());
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Search, ErrorType.DBError, ex.Message);
                }
            }
        }
    }
}
