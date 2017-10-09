using HikeHandler.Exceptions;
using HikeHandler.ModelObjects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace HikeHandler.DAOs
{
    public class CountryDao
    {
        private MySqlConnection sqlConnection;

        public CountryDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        // Recalculates the hike, region and cp count of every country in the DB.
        // Only for correcting erroneous hikecount data in the DB.
        public void RecalculateCountryData()
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            DataTable table = new DataTable();
            int id;
            string commandText = "SELECT countryID FROM country;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                if (!int.TryParse(row["countryID"].ToString(), out id))
                    throw new DBErrorException("'countryID' value should be an integer.");
                UpdateHikeCount(id);
                UpdateRegionCount(id);
                UpdateCPCount(id);
            }
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of hikecount.
        public int UpdateHikeCount(int countryID)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT SUM(counts) AS count FROM hike_country WHERE countryID=@countryID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@countryID", countryID);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT SUM(counts) return value should be integer.");
                commandText = "UPDATE country SET hikecount=@hikecount WHERE countryID=@countryID;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@hikecount", count);
                    updateCommand.Parameters.AddWithValue("@countryID", countryID);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Finds the correct region count, and stores it in the DB.
        // Returns the updated value of regioncount.
        public int UpdateRegionCount(int countryID)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT SUM(*) AS count FROM region_country WHERE countryID=@countryID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@countryID", countryID);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE country SET regioncount=@regioncount WHERE countryID=@countryID;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@regioncount", count);
                    updateCommand.Parameters.AddWithValue("@idcountry", countryID);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        public void UpdateRegionCount(List<int> countryIDs)
        {
            foreach (int id in countryIDs)
            {
                UpdateRegionCount(id);
            }
        }

        // Finds the correct cp count, and stores it in the DB.
        // Returns the updated value of cpcount.
        public int UpdateCPCount(int countryID)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM cp_country WHERE countryID=@countryID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@countryID", countryID);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE country SET cpcount=@cpcount WHERE countryID=@countryID;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@cpcount", count);
                    updateCommand.Parameters.AddWithValue("@countryID", countryID);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        public void UpdateCPCount(List<int> countryIDs)
        {
            foreach (int id in countryIDs)
            {
                UpdateCPCount(id);
            }
        }

        // Check whether there is a country in the DB with the given name. 
        // Returns true if there is.
        public bool IsDuplicateName(string countryName)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) FROM country WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", countryName);
                int count;
                if (!int.TryParse(command.ExecuteScalar().ToString(), out count))
                    throw new DBErrorException("'SELECT COUNT' return value should be an integer.");
                if (count == 0)
                    return false;
                if (count > 1)
                {
                    throw new DBErrorException("More than one country found with the same name.");
                }
                return true;
            }
        }

        // Checks whether the given country can be deleted.
        // Deletable only if no region, CP or hike belongs to it.
        public bool IsDeletable(int countryID)
        {
            CountryForView country = GetCountryByID(countryID);
            if (country.hikeCount > 0 || country.regionCount > 0 || country.cpCount > 0)
            {
                return false;
            }
            return true;
        }

        // Deletes the given country from DB.
        public void DeleteCountry(int countryID)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "DELETE FROM country WHERE countryID=@countryID";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@countryID", countryID);
                command.ExecuteNonQuery();
            }
        }

        // Saves country data to DB.
        public void SaveCountry (CountryForSave country)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"INSERT INTO country (name, hikecount, regioncount, cpcount, description) 
VALUES (@name, 0, 0, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.name);
                command.Parameters.AddWithValue("@description", country.description);
                command.ExecuteNonQuery();
            }
        }

        // Returns the data of the country with the given id.
        public CountryForView GetCountryByID(int countryID)
        {
            if (countryID <= 0)
            {
                throw new ArgumentException("countryID parameter should be positive.", "countryID");
            }
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = "SELECT name, description, hikecount, cpcount, regioncount FROM country WHERE countryID=@countryID;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@countryID", countryID);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    throw new NoItemFoundException();
                }
                if (table.Rows.Count > 1)
                {
                    throw new DBErrorException("More than one country found with the given ID.");
                }
                DataRow row = table.Rows[0];

                string name;
                string description;
                int hikeCount;
                int regionCount;
                int cpCount;

                if (!int.TryParse(row["hikecount"].ToString(), out hikeCount))
                {
                    throw new DBErrorException("country.hikecount should be an integer.");
                }
                if (!int.TryParse(row["regioncount"].ToString(), out regionCount))
                {
                    throw new DBErrorException("country.regioncount should be an integer.");
                }
                if (!int.TryParse(row["cpcount"].ToString(), out cpCount))
                {
                    throw new DBErrorException("country.cpcount should be an integer.");
                }
                name = row["name"].ToString();
                description = row["description"].ToString();

                return new CountryForView(countryID, name, hikeCount, regionCount, cpCount, description);
            }
        }

        // Updates the DB with data in the country object.
        public void UpdateCountry(CountryForUpdate country)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "UPDATE country SET name=@name, description=@description WHERE countryID=@countryID;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.newName);
                command.Parameters.AddWithValue("@description", country.description);
                command.Parameters.AddWithValue("@countryID", country.countryID);

                command.ExecuteNonQuery();
            }
        }
        
        // Performs a search in DB
        public List<CountryForView> SearchCountry(CountryForSearch template)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = 
                "SELECT countryID, name, hikecount, regioncount, cpcount, description FROM country WHERE name LIKE @name AND description LIKE @description";
            string hikeCountCondition = template.hikeCount.SqlSearchCondition("hikeCount");
            if (hikeCountCondition != String.Empty)
            {
                commandText += (" AND " + hikeCountCondition);
            }

            string cpCountCondition = template.cpCount.SqlSearchCondition("cpCount");
            if (cpCountCondition != String.Empty)
            {
                commandText += (" AND " + cpCountCondition);
            }

            string regionCountCondition = template.regionCount.SqlSearchCondition("regionCount");
            if (regionCountCondition != String.Empty)
            {
                commandText += (" AND " + regionCountCondition);
            }
            commandText += " ORDER BY name ASC;";

            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", "%" + template.name + "%");
                command.Parameters.AddWithValue("@description", "%" + template.description + "%");
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<CountryForView> resultList = new List<CountryForView>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int countryID = reader.GetInt32("countryID");
                            int hikeCount = reader.GetInt32("hikecount");
                            int cpCount = reader.GetInt32("cpcount");
                            int regionCount = reader.GetInt32("regioncount");
                            string name = reader.GetString("name");
                            string description = reader.GetString("description");
                            resultList.Add(new CountryForView(countryID, name, hikeCount, regionCount, cpCount, description));
                        }
                    }
                    return resultList;
                }
            }
        }
        
        // Gets the names and ids of all countries.
        public List<NameAndID> GetAllCountryNames()
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = "SELECT countryID, name FROM country ORDER BY name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                List<NameAndID> result = new List<NameAndID>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int countryID = reader.GetInt32("countryID");
                            string name = reader.GetString("name");
                            result.Add(new NameAndID(name, countryID));
                        }
                    }
                }
                return result;
            }
        }
        
        // Returns the number of countries in the DB.
        public int GetCountOfCountries()
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            int count;
            string commandText = "SELECT COUNT(*) FROM country;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                if (!int.TryParse(result.ToString(), out count))
                {
                    throw new DBErrorException("SELECT COUNT return value should be integer.");
                }
            }
            return count;
        }
    }
}
