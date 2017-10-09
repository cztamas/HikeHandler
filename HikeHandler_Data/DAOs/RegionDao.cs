using HikeHandler.Exceptions;
using HikeHandler.ModelObjects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace HikeHandler.DAOs
{
    public class RegionDao
    {
        private MySqlConnection sqlConnection;

        public RegionDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        private string GetSearchCommand(HikeRegionForSearch template)
        {
            string commandText = @"SELECT r.regionID, r.name, r.hikecount, r.cpcount, r.description,
 c.name AS countryname, c.countryID FROM region r, country c, region_country rc 
WHERE r.regionID = rc.regionID AND c.countryID = rc.countryID 
AND r.name LIKE @name AND c.name LIKE @cname AND r.description LIKE @description";
            if (template.hikeCount != null)
            {
                string countCondition = template.hikeCount.SqlSearchCondition("r.hikecount");
                if (countCondition != String.Empty)
                    commandText += (" AND " + countCondition);
            }
            if (template.cpCount != null)
            {
                string countCondition = template.cpCount.SqlSearchCondition("r.cpcount");
                if (countCondition != String.Empty)
                    commandText += (" AND " + countCondition);
            }
            if (template.countryID != null)
                commandText += (" AND c.idcountry=@idcountry");
            commandText += " ORDER BY name ASC;";
            return commandText;
        }

        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch template)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = GetSearchCommand(template);
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", "%" + template.name + "%");
                command.Parameters.AddWithValue("@cname", "%" + template.countryName + "%");
                command.Parameters.AddWithValue("@description", "%" + template.description + "%");
                if (template.countryID != null)
                    command.Parameters.AddWithValue("@idcountry", template.countryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return new List<HikeRegionForView>();
                    }
                    var dictionary = new Dictionary<int, HikeRegionForView>();
                    while(reader.Read())
                    {
                        int regionID = reader.GetInt32("regionID");
                        int countryID = reader.GetInt32("countryID");
                        string name = reader.GetString("name");
                        int hikeCount = reader.GetInt32("hikecount");
                        int cpCount = reader.GetInt32("cpcount");
                        string countryName = reader.GetString("countryname");
                        string description = reader.GetString("description");
                        HikeRegionForView item;
                        if (dictionary.TryGetValue(regionID, out item))
                        {
                            item.countryIDs.Add(countryID);
                            item.countryNames.Add(countryName);
                        }
                        else
                        {
                            HikeRegionForView newRegion = new HikeRegionForView(
                                regionID, name, new List<int> { countryID }, new List<string> { countryName }, hikeCount, cpCount, description);
                            dictionary.Add(regionID, newRegion);
                        }
                    }
                    return new List<HikeRegionForView>(dictionary.Values);
                }
            }
        }
        
        public HikeRegionForView GetRegionByID(int regionID)
        {
            if (regionID <= 0)
            {
                throw new ArgumentException("regionID parameter should be positive.", "regionID");
            }
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText =
                @"SELECT r.name, r.description, r.hikecount, r.cpcount, c.countryID, c.name AS countryname
FROM region r, region_country rc, country c 
WHERE r.regionID = @regionID AND rc.regionID = r.regionID AND rc.countryID = c.countryID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@regionID", regionID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new NoItemFoundException();
                    }
                    var countryIDs = new List<int>();
                    var countryNames = new List<string>();
                    int hikeCount = 0;
                    int cpCount = 0;
                    string name = String.Empty;
                    string description = String.Empty;
                    while (reader.Read())
                    {
                        int countryID = reader.GetInt32("countryID");
                        countryIDs.Add(countryID);
                        string countryName = reader.GetString("countryname");
                        countryNames.Add(countryName);
                        name = reader.GetString("name");
                        description = reader.GetString("description");
                        hikeCount = reader.GetInt32("hikecount");
                        cpCount = reader.GetInt32("cpcount");
                    }
                    return new HikeRegionForView(regionID, name, countryIDs, countryNames, hikeCount, cpCount, description);
                }
            }
        }

        // Recalculates the hike count of every region in the DB.
        // Only for correcting erroneous data in the DB.
        public void RecalculateRegionData()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            DataTable table = new DataTable();
            int id;
            string commandText = "SELECT idregion FROM region;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                if (!int.TryParse(row["idregion"].ToString(), out id))
                {
                    throw new DBErrorException("'idregion' value should be an integer.");
                }
                UpdateHikeCount(id);
                UpdateCPCount(id);
            }
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount.
        public int UpdateHikeCount(int regionID)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idregion=" + regionID + " AND type='túra';";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE region SET hikecount=@hikecount WHERE idregion=@idregion;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@hikecount", count);
                    updateCommand.Parameters.AddWithValue("@idregion", regionID);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Finds the correct cp count, and stores it in the DB.
        // Returns the updated value of cpcount.
        public int UpdateCPCount(int regionID)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idregion=@regionID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@regionID", regionID);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("'SELECT COUNT' return value should be an integer.");

                commandText = "UPDATE region SET cpcount=@cpcount WHERE idregion=@regionID;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@cpcount", count);
                    updateCommand.Parameters.AddWithValue("@regionID", regionID);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        public void UpdateCPCount(List<int> regionIDs)
        {
            foreach (int item in regionIDs)
            {
                UpdateCPCount(item);
            }
        }

        // Checks whether the given region can be deleted.
        // Deletable only if no CP or hike belongs to it.
        public bool IsDeletable(int regionID)
        {
            HikeRegionForView region = GetRegionData(regionID);
            if (region.HikeCount > 0 || region.CPCount > 0)
                return false;
            else
                return true;
        }

        // Returns the data of the region with the given id.
        public HikeRegionForView GetRegionData(int regionID)
        {
            if (regionID <= 0)
            {
                throw new ArgumentException("regionID parameter should be positive.", "regionID");
            }
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = @"SELECT r.idcountry, r.name, r.hikecount, r.cpcount, r.description, c.name AS countryname 
FROM region r, country c WHERE c.idcountry=r.idcountry AND r.idregion=@idregion;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idregion", regionID);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    throw new NoItemFoundException();
                }
                if (table.Rows.Count > 1)
                {
                    throw new DBErrorException("More than one region found with the given id.");
                }
                DataRow row = table.Rows[0];

                string name;
                string countryName;
                string description;
                int countryID;
                int hikeCount;
                int cpCount;

                if (!int.TryParse(row["hikecount"].ToString(), out hikeCount))
                {
                    throw new DBErrorException("'region.hikecount' should be an integer.");
                }
                if (!int.TryParse(row["cpcount"].ToString(), out cpCount))
                {
                    throw new DBErrorException("'region.cpcount' should be an integer.");
                }
                if (!int.TryParse(row["idcountry"].ToString(), out countryID))
                {
                    throw new DBErrorException("'region.idcountry' should be an integer.");
                }
                name = row["name"].ToString();
                description = row["description"].ToString();
                countryName = row["countryname"].ToString();

                return new HikeRegionForView(regionID, countryID, name, countryName, hikeCount, cpCount, description);
            }
        }

        // Returns in a list with the names and ids of every region of the given country.
        public List<NameAndID> GetRegionNames(int countryID)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT idregion, name FROM region WHERE idcountry=@idcountry ORDER BY name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                List<NameAndID> result = new List<NameAndID>();
                command.Parameters.AddWithValue("@idcountry", countryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("idregion");
                            string name = reader.GetString("name");
                            result.Add(new NameAndID(name, id));
                        }
                    }
                    else
                        return result;
                }
                return result;
            }
        }

        // Returns in a list with the names and ids of every region in the DB.
        public List<NameAndID> GetRegionNames()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT idregion, name FROM region ORDER BY name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                List<NameAndID> result = new List<NameAndID>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("idregion");
                            string name = reader.GetString("name");
                            result.Add(new NameAndID(name, id));
                        }
                    }
                    else
                        return result;
                }
                return result;
            }
        }

        public bool IsDuplicateName(string regionName)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) FROM region WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", regionName);
                int count;
                object result = command.ExecuteScalar();
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("'SELECT COUNT' return value should be an integer.");
                if (count == 0)
                    return false;
                if (count > 1)
                {
                    throw new DBErrorException("More than one region found with the same name.");
                }
                return true;
            }
        }

        public void DeleteRegion(int regionID)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "DELETE FROM region WHERE idregion=@idregion";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idregion", regionID);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRegion(HikeRegionForUpdate regionData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "UPDATE region SET name=@name, description=@description WHERE idregion=@idregion;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", regionData.NewName);
                command.Parameters.AddWithValue("@idregion", regionData.RegionID);
                command.Parameters.AddWithValue("@description", regionData.Description);
                command.ExecuteNonQuery();
            }
        }

        public void SaveRegion(HikeRegionForSave regionData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"INSERT INTO region (name, idcountry, hikecount, cpcount, description) 
VALUES (@name, @idCountry, 0, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", regionData.Name);
                command.Parameters.AddWithValue("@idCountry", regionData.CountryID);
                command.Parameters.AddWithValue("@description", regionData.Description);
                command.ExecuteNonQuery();
            }
        }

        // Returns the number of regions in the DB.
        public int GetCountOfRegions()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            int count;
            string commandText = "SELECT COUNT(*) FROM region;";
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
    }
}
