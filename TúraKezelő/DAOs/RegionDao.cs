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

        // Recalculates the hike count of every region in the DB.
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
            string commandText = "SELECT idregion FROM region;";
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
                id = int.Parse(row["idregion"].ToString());
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
        public int UpdateHikeCount(int idRegion)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idregion=" + idRegion + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return -1;
                    commandText = "UPDATE region SET hikecount=@hikecount WHERE idregion=@idregion;";
                    using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idregion", idRegion);
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

        private bool IsDeletable(int idRegion)
        {
            if (UpdateHikeCount(idRegion) != 0)
                return false;
            if (CountCPs(idRegion) != 0)
                return false;
            return true;
        }

        // Returns the number of checkpoints corresponding to the given region.
        private int CountCPs(int idRegion)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.CountCPs, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.CountCPs, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idregion=@idregion;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idregion", idRegion);
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                        return -1;
                    return count;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.CountCPs, ErrorType.DBError, ex.Message);
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
            string commandText = "SELECT COUNT(*) FROM region WHERE name=@name;";
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

        public bool DeleteRegion(int idRegion)
        {
            if (!IsDeletable(idRegion))
            {
                throw new DaoException(ActivityType.Delete, ErrorType.NotDeletable, string.Empty);
            }
            string commandText = "DELETE FROM region WHERE idregion=@idregion";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idregion", idRegion);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Delete, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool UpdateRegion(HikeRegion regionData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (IsDuplicateName(regionData.Name))
            {
                throw new DaoException(ActivityType.Update, ErrorType.DuplicateName, string.Empty);
            }
            string commandText = "UPDATE region SET name=@name, description=@description WHERE idregion=@idregion;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", regionData.Name);
                command.Parameters.AddWithValue("@idregion", regionData.ID);
                command.Parameters.AddWithValue("@description", regionData.Description);
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

        public bool SaveRegion(HikeRegion regionData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (IsDuplicateName(regionData.Name))
            {
                throw new DaoException(ActivityType.Save, ErrorType.DuplicateName, string.Empty);
            }
            string commandText = @"INSERT INTO region (name, idcountry, hikecount, description) 
VALUES (@name, @idCountry, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", regionData.Name);
                command.Parameters.AddWithValue("@idCountry", regionData.CountryID);
                command.Parameters.AddWithValue("@description", regionData.Description);
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
