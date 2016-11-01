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

        private void MovePositions(DateTime date, bool upOrDown)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.MoveHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.MoveHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "UPDATE hike SET position=position+1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";
            if (!upOrDown)
                commandText = "UPDATE hike SET position=position-1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.MoveHikePositions, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Recalculates the position of every hike in the DB.
        // Only for correcting erroneous data in the DB.
        public void RecalculatePositions()
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            DataTable hikesTable = new DataTable();
            DataTable table = new DataTable();
            DateTime date;
            string commandText = "SELECT idhike, date FROM hike WHERE type='túra';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    adapter.Fill(hikesTable);
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, ex.Message);
                }
            }
            //MessageBox.Show(hikesTable.Rows.Count.ToString());
            foreach (DataRow row in hikesTable.Rows)
            {
                int hikeID = int.Parse(row["idhike"].ToString());
                date = Convert.ToDateTime(row["date"]);
                commandText = "SELECT COUNT(*) AS count FROM hike WHERE date < '"
                + date.ToString("yyyy-MM-dd") + "' AND type='túra';";
                using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                {
                    try
                    {
                        object result = command.ExecuteScalar();
                        int count;
                        if (!int.TryParse(result.ToString(), out count))
                            throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, string.Empty);
                        count++;
                        commandText = "UPDATE hike SET position=" + count + " WHERE idhike=" + hikeID + " AND type='túra';";
                        using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                        {
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, ex.Message);
                    }
                }
            }
        }

        public bool DeleteHike(Hike hikeData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Delete, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Delete, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "DELETE FROM hike WHERE idhike=@idhike";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idhike", hikeData.IDHike);
                    command.ExecuteNonQuery();
                    MovePositions(hikeData.HikeDate, false);
                    CPDao cpDao = new CPDao(sqlConnection);
                    CountryDao countryDao = new CountryDao(sqlConnection);
                    RegionDao regionDao = new RegionDao(sqlConnection);
                    foreach (int item in hikeData.CPList)
                    {
                        cpDao.UpdateHikeCount(item);
                    }
                    regionDao.UpdateHikeCount(hikeData.IDRegion);
                    countryDao.UpdateHikeCount(hikeData.IDCountry);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Delete, ErrorType.DBError, ex.Message);
                }
            }
        }

        private bool IsDuplicateDate(DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.CheckDuplicateDate, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.CheckDuplicateDate, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) FROM hike WHERE date=@date;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
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
                    throw new DaoException(ActivityType.CheckDuplicateDate, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool SaveHike(Hike hikeData)
        {
            if (IsDuplicateDate(hikeData.HikeDate))
            {
                throw new DaoException(ActivityType.Save, ErrorType.DuplicateDate, string.Empty);
            }
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = @"INSERT INTO hike (date, idregion, idcountry, type, description, cpstring) 
VALUES (@date, @idregion, @idcountry, @type, @description, @cpstring)";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", hikeData.HikeDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@idregion", hikeData.IDRegion);
                command.Parameters.AddWithValue("@idcountry", hikeData.IDCountry);
                command.Parameters.AddWithValue("@description", hikeData.Description);
                command.Parameters.AddWithValue("@type", hikeData.HikeType.ToString());
                command.Parameters.AddWithValue("@cpstring", hikeData.GetCPString());
                try
                {
                    command.ExecuteNonQuery();
                    CountryDao countryDao = new CountryDao(sqlConnection);
                    RegionDao regionDao = new RegionDao(sqlConnection);
                    CPDao cpDao = new CPDao(sqlConnection);
                    if (hikeData.HikeType == HikeType.túra)
                    {
                        InsertIntoPositionList(hikeData.IDHike, hikeData.HikeDate);
                        countryDao.UpdateHikeCount(hikeData.IDCountry);
                        regionDao.UpdateHikeCount(hikeData.IDRegion);
                        foreach (int item in hikeData.CPList)
                        {
                            cpDao.UpdateHikeCount(item);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Save, ErrorType.DBError, ex.Message);
                }
            }
        }

        public bool UpdateHike(Hike newHikeData, Hike oldHikeData)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            bool dateChanged = (oldHikeData.HikeDate != newHikeData.HikeDate);
            bool typeChanged = (oldHikeData.HikeType != newHikeData.HikeType);
            if (dateChanged && IsDuplicateDate(newHikeData.HikeDate))
            {
                throw new DaoException(ActivityType.Update, ErrorType.DuplicateDate, string.Empty);
            }

            CountryDao countryDao = new CountryDao(sqlConnection);
            RegionDao regionDao = new RegionDao(sqlConnection);
            CPDao cpDao = new CPDao(sqlConnection);
            string commandText = @"UPDATE hike SET date=@date, description=@description, type=@type, cpstring=@cpstring
                WHERE idhike=@idhike;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", newHikeData.HikeDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@description", newHikeData.Description);
                command.Parameters.AddWithValue("@type", newHikeData.HikeType.ToString());
                command.Parameters.AddWithValue("@cpstring", newHikeData.GetCPString());
                command.Parameters.AddWithValue("@idhike", newHikeData.IDHike);
                try
                {
                    command.ExecuteNonQuery();
                    if ((typeChanged || dateChanged) && oldHikeData.HikeType == HikeType.túra) 
                    {
                        RemoveFromPositionList(oldHikeData.IDHike, oldHikeData.HikeDate);
                    }
                    if (newHikeData.HikeType == HikeType.túra)
                    {
                        InsertIntoPositionList(newHikeData.IDHike, newHikeData.HikeDate);
                    }
                    if (typeChanged)
                    {
                        countryDao.UpdateHikeCount(newHikeData.IDCountry);
                        countryDao.UpdateHikeCount(oldHikeData.IDCountry);
                        regionDao.UpdateHikeCount(newHikeData.IDRegion);
                        regionDao.UpdateHikeCount(oldHikeData.IDRegion);
                    }
                    foreach (int item in oldHikeData.CPList)
                        cpDao.UpdateHikeCount(item);
                    foreach (int item in newHikeData.CPList)
                        cpDao.UpdateHikeCount(item);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Update, ErrorType.DBError, ex.Message);
                }
            }
        }

        private void RemoveFromPositionList(int hikeID, DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "UPDATE hike SET position = NULL WHERE idhike=" + hikeID + ";";
            using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    updateCommand.ExecuteNonQuery();
                    MovePositions(date, false);
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, ex.Message);
                }
            }
        }

        private void InsertIntoPositionList(int hikeID, DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE date < '" 
                + date.ToString("yyyy-MM-dd") + "' AND type='túra';";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                        throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, string.Empty);
                    count++;
                    commandText = "UPDATE hike SET position=" + count + " WHERE idhike=" + hikeID + " AND type='túra';";
                    using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                    {
                        updateCommand.ExecuteNonQuery();
                    }
                    MovePositions(date, true);
                }
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.UpdateHikePositions, ErrorType.DBError, ex.Message);
                }
            }
        }
    }
}
