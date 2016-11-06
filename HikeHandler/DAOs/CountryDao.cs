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

        // Recalculates the hike count of every country in the DB.
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
            string commandText = "SELECT idcountry FROM country;";
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
                id = int.Parse(row["idcountry"].ToString());
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

        // Check whether there is a country in the DB with the given name. Returns true if there is.
        // Throws CountryDaoException in case of an error.
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
                    throw new DBErrorException("SELECT COUNT return value should be integer.");
                if (count == 0)
                    return false;
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

        // Deletes the given country from DB, or throws CountryDaoException in case of an error.
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
        public bool SaveCountry (CountryForSave country)
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
                return true;
            }
        }

        // Returns the data of the country with given id, or throws CountryDaoException in case of an error.
        public CountryForView GetCountryData(int idCountry)
        {
            if (idCountry <= 0)
            {
                throw new ArgumentException("idCountry parameter should be positive.", "idCountry");
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
                adapter.SelectCommand.Parameters.AddWithValue("@id", idCountry);
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
                    new CountryForView(idCountry, name, hikeCount, regionCount, cpCount, description);
                return countryData;
            }
        }

        // Updates the DB with data in the country object, or throws CountryDaoException in case of an error.
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
        
        public DataTable SearchCountry(CountryForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new DaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT idcountry, name, hikecount FROM country WHERE name LIKE @name";
            string countCondition = template.HikeCount.SqlSearchCondition("hikeCount");
            if (countCondition != String.Empty)
                commandText += (" AND " + countCondition);
            commandText += " ORDER BY name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + template.Name + "%");
                DataTable table = new DataTable();
                try
                {
                    adapter.Fill(table);
                    return table;
                }   
                catch (Exception ex)
                {
                    throw new DaoException(ActivityType.Search, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Gets the list of all countries in a datatable
        public DataTable GetCountryTable()
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
    }
}
