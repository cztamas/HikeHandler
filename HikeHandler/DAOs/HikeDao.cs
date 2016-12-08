using HikeHandler.Exceptions;
using HikeHandler.ModelObjects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HikeHandler.DAOs
{
    public class HikeDao
    {
        private MySqlConnection sqlConnection;

        public HikeDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        private string GetSearchCommandText(HikeForSearch template)
        {
            string commandText = @"SELECT h.idhike, h.position, h.date, h.idregion, r.name AS 'regionname', h.idcountry, 
c.name AS 'countryname', h.type, h.description, h.cpstring FROM hike h, region r, country c WHERE h.idcountry=c.idcountry 
AND h.idregion=r.idregion AND c.name LIKE @countryName AND r.name LIKE @regionName";
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
            if (template.AnyCPOrder)
            {
                foreach (int item in template.CPList)
                {
                    commandText += " AND cpstring LIKE '%." + item.ToString() + ".%'";
                }
            }
            if (!template.AnyCPOrder)
            {
                string condition = string.Empty;
                foreach (int item in template.CPList)
                {
                    condition += "%." + item + ".%";
                }
                commandText += " AND cpstring LIKE '" + condition + "'";
            }
            commandText += " ORDER BY h.date ASC;";
            return commandText;
        }

        public List<HikeForView> SearchHike(HikeForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = GetSearchCommandText(template);
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@countryName", "%" + template.CountryName + "%");
                command.Parameters.AddWithValue("@regionName", "%" + template.RegionName + "%");
                if (template.HikeType != null)
                {
                    command.Parameters.AddWithValue("@type", template.HikeType.ToString());
                }
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<HikeForView> resultList = new List<HikeForView>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int hikeID = reader.GetInt32("idhike");
                            int countryID = reader.GetInt32("idcountry");
                            int regionID = reader.GetInt32("idregion");
                            // Position entry in DB may be Null.
                            int? position = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("position")))
                            {
                                position = reader.GetInt32("position");
                            }
                            string countryName = reader.GetString("countryname");
                            string regionName = reader.GetString("regionname");
                            string description = reader.GetString("description");
                            string cpString = reader.GetString("cpstring");
                            HikeType hikeType;
                            if (!Enum.TryParse(reader.GetString("type"), out hikeType))
                            {
                                throw new DBErrorException("Invalid hiketype found.");
                            }
                            DateTime hikeDate;
                            if (!DateTime.TryParse(reader.GetString("date"), out hikeDate))
                            {
                                throw new DBErrorException("Invalid date format.");
                            }
                            resultList.Add(new HikeForView(hikeID, countryID, regionID, position, countryName, regionName,
                                description, hikeDate, hikeType, cpString));
                        }
                    }
                    else
                    {
                        return resultList;
                    }
                    return resultList;
                }
            }
        }

        // Returns the data of the hike with the given id.
        public HikeForView GetHikeData(int hikeID)
        {
            if (hikeID <= 0)
            {
                throw new ArgumentException("hikeID parameter should be positive.", "cpID");
            }
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = @"SELECT hike.idregion, hike.idcountry, hike.date, hike.position, hike.cpstring, hike.type, hike.description, 
r.name AS regionname, c.name AS countryname FROM hike, region r, country c WHERE hike.idregion=r.idregion AND hike.idcountry=c.idcountry AND hike.idhike=@idhike;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idhike", hikeID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new NoItemFoundException();
                    }
                    bool isFirstItem = true;
                    while (reader.Read())
                    {
                        if (!isFirstItem)
                        {
                            throw new DBErrorException("More than one hike found with the given id.");
                        }
                        isFirstItem = false;
                        
                        int countryID = reader.GetInt32("idcountry");
                        int regionID = reader.GetInt32("idregion");
                        // Position entry in DB may be Null.
                        int? position = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("position")))
                        {
                            position = reader.GetInt32("position");
                        }
                        string countryName = reader.GetString("countryname");
                        string regionName = reader.GetString("regionname");
                        string description = reader.GetString("description");
                        string cpString = reader.GetString("cpstring");
                        HikeType hikeType;
                        if (!Enum.TryParse(reader.GetString("type"), out hikeType))
                        {
                            throw new DBErrorException("Invalid hiketype found.");
                        }
                        DateTime hikeDate;
                        if (!DateTime.TryParse(reader.GetString("date"), out hikeDate))
                        {
                            throw new DBErrorException("Invalid date format.");
                        }
                        if (!cpString.IsCPString())
                        {
                            throw new DBErrorException("'hike.cpstring' value not valid.");
                        }
                        return new HikeForView(hikeID, countryID, regionID, position, countryName, regionName,
                            description, hikeDate, hikeType, cpString);
                    }
                    throw new NoItemFoundException();
                }
            }
        }

        public void MovePositions(DateTime date, bool upOrDown)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "UPDATE hike SET position=position+1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";
            if (!upOrDown)
                commandText = "UPDATE hike SET position=position-1 WHERE date > @date AND type='túra' AND position IS NOT NULL;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
            }
        }

        // Recalculates the position of every hike in the DB.
        // Only for correcting erroneous data in the DB.
        public void RecalculatePositions()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            DataTable hikesTable = new DataTable();
            DataTable table = new DataTable();
            DateTime date;
            string commandText = "SELECT idhike, date FROM hike WHERE type='túra';";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.Fill(hikesTable);
            }
            foreach (DataRow row in hikesTable.Rows)
            {
                int hikeID = int.Parse(row["idhike"].ToString());
                if (!DateTime.TryParse(row["date"].ToString(), out date))
                {
                    throw new DBErrorException("Invalid 'date' parameter format.");
                }
                commandText = "SELECT COUNT(*) AS count FROM hike WHERE date < '"
                + date.ToString("yyyy-MM-dd") + "' AND type='túra';";
                using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                {
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                    {
                        throw new DBErrorException("'SELECT COUNT' result should be an integer.");
                    }
                    count++;
                    commandText = "UPDATE hike SET position=" + count + " WHERE idhike=" + hikeID + " AND type='túra';";
                    using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                    {
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteHike(HikeForView hikeData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "DELETE FROM hike WHERE idhike=@idhike";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idhike", hikeData.HikeID);
                command.ExecuteNonQuery();



                MovePositions(hikeData.HikeDate, false);
                CPDao cpDao = new CPDao(sqlConnection);
                CountryDao countryDao = new CountryDao(sqlConnection);
                RegionDao regionDao = new RegionDao(sqlConnection);
                foreach (int item in hikeData.CPList)
                {
                    cpDao.UpdateHikeCount(item);
                }
                regionDao.UpdateHikeCount(hikeData.RegionID);
                countryDao.UpdateHikeCount(hikeData.CountryID);
            }
        }

        public bool IsDuplicateDate(DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) FROM hike WHERE date=@date;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                int count;
                object result = command.ExecuteScalar();
                if (!int.TryParse(result.ToString(), out count))
                {
                    throw new DBErrorException("'SELECT COUNT' result should be an integer.");
                }
                if (count == 0)
                    return false;
                if (count > 1)
                {
                    throw new DBErrorException("More than one hike found with the given date.");
                }
                return true;
            }
        }

        // Saves hike data to DB.
        public void SaveHike(HikeForSave hikeData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"INSERT INTO hike (date, idregion, idcountry, type, description, cpstring) 
VALUES (@date, @idregion, @idcountry, @type, @description, @cpstring)";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", hikeData.HikeDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@idregion", hikeData.RegionID);
                command.Parameters.AddWithValue("@idcountry", hikeData.CountryID);
                command.Parameters.AddWithValue("@description", hikeData.Description);
                command.Parameters.AddWithValue("@type", hikeData.HikeType.ToString());
                command.Parameters.AddWithValue("@cpstring", hikeData.CPString);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateHike(HikeForUpdate hike)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText =
                @"UPDATE hike SET date=@date, description=@description, type=@type, cpstring=@cpstring WHERE idhike=@idhike;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@date", hike.NewHikeDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@description", hike.Description);
                command.Parameters.AddWithValue("@type", hike.NewHikeType.ToString());
                command.Parameters.AddWithValue("@cpstring", hike.NewCPString);
                command.Parameters.AddWithValue("@idhike", hike.HikeID);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveFromPositionList(int hikeID, DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "UPDATE hike SET position = NULL WHERE idhike=" + hikeID + ";";
            using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
            {
                int updatedRows = updateCommand.ExecuteNonQuery();
                if (updatedRows == 0)
                {
                    throw new ArgumentException("No hike found with the given date.", "date");
                }
                if (updatedRows > 1)
                {
                    throw new DBErrorException("More than one hikes found with the given date.");
                }
                MovePositions(date, false);
            }
        }

        public void InsertIntoPositionList(DateTime date)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE date < '"
                + date.ToString("yyyy-MM-dd") + "' AND type='túra';";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                {
                    throw new DBErrorException("'SELECT COUNT' result should be an integer.");
                }
                count++;
                commandText = "UPDATE hike SET position=" + count + " WHERE date='" + date.ToString("yyyy-MM-dd") + "' AND type='túra';";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    int updatedRows = updateCommand.ExecuteNonQuery();
                    if (updatedRows == 0)
                    {
                        throw new ArgumentException("No hike found with the given date.", "date");
                    }
                    if (updatedRows > 1)
                    {
                        throw new DBErrorException("More than one hikes found with the given date.");
                    }
                }
                MovePositions(date, true);
            }
        }

        // Returns the number of hikes in the DB.
        public int GetCountOfHikes(bool hikeOnly)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            int count;
            string commandText = "SELECT COUNT(*) FROM hike;";
            if (hikeOnly)
            {
                commandText = "SELECT COUNT(*) FROM hike WHERE type='túra';";
            }
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                if (!int.TryParse(result.ToString(), out count))
                {
                    throw new DBErrorException("'SELECT COUNT' return value should be an integer.");
                }
            }
            return count;
        }

        public List<NameAndID> GetHikeTypes()
        {
            List<NameAndID> result = new List<NameAndID>();
            Array hikeTypes = Enum.GetValues(typeof(HikeType));
            foreach (HikeType item in hikeTypes)
            {
                result.Add(new NameAndID(item.ToString(), (int)item));
            }
            return result;
        }
    }
}
