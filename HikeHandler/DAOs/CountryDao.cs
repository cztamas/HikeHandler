using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HikeHandler.ModelObjects;
using HikeHandler.Exceptions;

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
        // Only for correcting erroneous data in the DB.
        public void RecalculateCountryData()
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
            string commandText = "SELECT idcountry FROM country;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                if (!int.TryParse(row["idcountry"].ToString(), out id))
                    throw new DBErrorException("'idcountry' value should be an integer.");
                UpdateHikeCount(id);
                UpdateRegionCount(id);
                UpdateCPCount(id);
            }
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of hikecount.
        public int UpdateHikeCount(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idcountry=" + idCountry + ";";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");
                commandText = "UPDATE country SET hikecount=@hikecount WHERE idcountry=@idcountry;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@hikecount", count);
                    updateCommand.Parameters.AddWithValue("@idcountry", idCountry);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Finds the correct region count, and stores it in the DB.
        // Returns the updated value of regioncount.
        public int UpdateRegionCount(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM region WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idcountry", idCountry);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE country SET regioncount=@regioncount WHERE idcountry=@idcountry;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@regioncount", count);
                    updateCommand.Parameters.AddWithValue("@idcountry", idCountry);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Finds the correct cp count, and stores it in the DB.
        // Returns the updated value of cpcount.
        public int UpdateCPCount(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idcountry", idCountry);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE country SET cpcount=@cpcount WHERE idcountry=@idcountry;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@cpcount", count);
                    updateCommand.Parameters.AddWithValue("@idcountry", idCountry);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Check whether there is a country in the DB with the given name. 
        // Returns true if there is.
        public bool IsDuplicateName(string countryName)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
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
        public bool IsDeletable(int idCountry)
        {
            CountryForView country = GetCountryData(idCountry);
            if (country.HikeCount > 0 || country.RegionCount > 0 || country.CPCount > 0)
                return false;
            else
                return true;
        }

        // Deletes the given country from DB.
        public void DeleteCountry(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "DELETE FROM country WHERE idcountry=@idcountry";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idcountry", idCountry);
                command.ExecuteNonQuery();
            }
        }

        // Saves country data to DB.
        public void SaveCountry (CountryForSave country)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"INSERT INTO country (name, hikecount, regioncount, cpcount, description) 
VALUES (@name, 0, 0, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.Name);
                command.Parameters.AddWithValue("@description", country.Description);
                command.ExecuteNonQuery();
            }
        }

        // Returns the data of the country with the given id.
        public CountryForView GetCountryData(int countryID)
        {
            if (countryID <= 0)
            {
                throw new ArgumentException("countryID parameter should be positive.", "countryID");
            }
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = "SELECT name, description, hikecount, cpcount, regioncount FROM country WHERE idcountry=@id;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@id", countryID);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    return null; 
                }
                if (table.Rows.Count > 1)
                {
                    throw new DBErrorException("More than one country found with the given id.");
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

                CountryForView countryData =
                    new CountryForView(countryID, name, hikeCount, regionCount, cpCount, description);
                return countryData;
            }
        }

        // Updates the DB with data in the country object.
        public void UpdateCountry(CountryForUpdate country)
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
                "UPDATE country SET NAME=@name, DESCRIPTION=@description WHERE IDCOUNTRY=@id;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.NewName);
                command.Parameters.AddWithValue("@description", country.Description);
                command.Parameters.AddWithValue("@id", country.CountryID);

                command.ExecuteNonQuery();
            }
        }
        
        // Performs a search in DB
        public DataTable SearchCountry(CountryForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT idcountry, name, hikecount, regioncount, cpcount FROM country WHERE name LIKE @name";
            string hikeCountCondition = template.HikeCount.SqlSearchCondition("hikeCount");
            if (hikeCountCondition != String.Empty)
                commandText += (" AND " + hikeCountCondition);

            string cpCountCondition = template.CPCount.SqlSearchCondition("cpCount");
            if (cpCountCondition != String.Empty)
                commandText += (" AND " + cpCountCondition);

            string regionCountCondition = template.RegionCount.SqlSearchCondition("regionCount");
            if (regionCountCondition != String.Empty)
                commandText += (" AND " + regionCountCondition);
            commandText += " ORDER BY name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + template.Name + "%");
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // Gets the names and ids of all countries.
        public DataTable GetCountryNameTable()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT idcountry, name FROM country ORDER BY name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // Returns the number of countries in the DB.
        public int GetCountOfCountries()
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
