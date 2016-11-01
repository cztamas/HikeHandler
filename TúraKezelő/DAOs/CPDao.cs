using HikeHandler.Data_Containers;
using HikeHandler.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable SearchCP(CPTemplate template)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = @"SELECT cp.idcp, cp.name, cp.type, cp.hikecount, r.name, c.name, cp.description
FROM cp, region r, country c WHERE cp.idregion=r.idregion AND cp.idcountry=c.idcountry
AND cp.name LIKE @name AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (template.CPID != null)
                commandText += " AND cp.idcp=" + template.CPID;
            if (template.IDRegion != null)
                commandText += " AND r.idregion=" + template.IDRegion;
            if (template.IDCountry != null)
                commandText += " AND c.idcountry=" + template.IDCountry;
            if (template.HikeCount != null)
            {
                if (template.HikeCount.Count() > 0)
                    commandText += " AND " + template.HikeCount.SqlSearchCondition("cp.hikecount");
            }
            if (template.TypeOfCP != null)
                commandText += " AND cp.type=@type";
            commandText += " ORDER BY cp.name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + template.Name + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@countryName", "%" + template.CountryName + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@regionName", "%" + template.RegionName + "%");
                if (template.TypeOfCP != null)
                    adapter.SelectCommand.Parameters.AddWithValue("@type", template.TypeOfCP.ToString());
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

        // Recalculates the hike count of every cp in the DB.
        // Only for correcting erroneous data in the DB.
        public void RecalculateHikeCounts()
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            DataTable table = new DataTable();
            int id;
            string commandText = "SELECT idcp FROM cp;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.DBError, ex.Message);
                }
            }
            foreach (DataRow row in table.Rows)
            {
                id = int.Parse(row["idcp"].ToString());
                try
                {
                    UpdateHikeCount(id);
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount, or throws DaoException in case of an error.
        public int UpdateHikeCount(int idCP)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE cpstring LIKE '%." + idCP + ".%';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return -1;
                    commandText = "UPDATE cp SET hikecount=@hikecount WHERE idcp=@idcp;";
                    using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idcp", idCP);
                        command.ExecuteNonQuery();
                        return count;
                    }
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool IsDeletable(int idCP)
        {
            if (UpdateHikeCount(idCP) != 0)
                return false;
            return true;
        }

        public bool DeleteCP(int idCP)
        {
            if (!IsDeletable(idCP))
            {
                throw new DaoException(ActivityType.Delete, ErrorType.NotDeletable, string.Empty);
            }
            string commandText = "DELETE FROM cp WHERE idcp=@idcp";
            using (MySqlCommand command = new MySqlCommand(commandText,sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcp", idCP);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Delete, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool IsDuplicateName(string name)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.CheckDuplicateName, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.CheckDuplicateName, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) FROM cp WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    int count;
                    if (!int.TryParse(command.ExecuteScalar().ToString(), out count))
                        return true;
                    if (count == 0)
                        return false;
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.CheckDuplicateName, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool UpdateCP(CP cpData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (IsDuplicateName(cpData.Name))
            {
                throw new DaoException(ActivityType.Update, ErrorType.DuplicateName, string.Empty);
            }
            string commandText = "UPDATE cp SET name=@name, type=@type, description=@description WHERE idcp=@idcp;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", cpData.Name);
                string typeString = string.Empty;
                if (cpData.TypeOfCP != null)
                    typeString = cpData.TypeOfCP.ToString();
                command.Parameters.AddWithValue("@type", typeString);
                command.Parameters.AddWithValue("@description", cpData.Description);
                command.Parameters.AddWithValue("@idcp", cpData.CPID);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Update, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool SaveCP(CP cpData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (IsDuplicateName(cpData.Name))
            {
                throw new DaoException(ActivityType.Save, ErrorType.DuplicateName, string.Empty);
            }
            string commandText = @"INSERT INTO cp (name, idcountry, idregion, type, hikecount, description) 
VALUES (@name, @idcountry, @idregion, @type, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", cpData.Name);
                command.Parameters.AddWithValue("@idcountry", cpData.IDCountry);
                command.Parameters.AddWithValue("@idregion", cpData.IDRegion);
                string typeString = string.Empty;
                if (cpData.TypeOfCP != null)
                    typeString = cpData.TypeOfCP.ToString();
                command.Parameters.AddWithValue("@type", typeString);
                command.Parameters.AddWithValue("@description", cpData.Description);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Save, ErrorType.DBError, ex.Message);
                }
            }
        }
    }
}
