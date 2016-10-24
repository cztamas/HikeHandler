﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HikeHandler.Data_Containers;
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

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount, or throws CountryDaoException in case of an error.
        public int UpdateHikeCount(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.UpdateHikeCount, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idcountry=" + idCountry + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    int count;
                    if (!int.TryParse(table.Rows[0]["count"].ToString(), out count))
                        return -1;
                    commandText = "UPDATE country SET hikecount=@hikecount WHERE idcountry=@idcountry;";
                    using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@hikecount", count);
                        command.Parameters.AddWithValue("@idcountry", idCountry);
                        command.ExecuteNonQuery();
                        return count;
                    }
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.UpdateHikeCount, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Returns the number of regions corresponding to the given country, 
        // or throws CountryDaoException in case of an error.
        public int CountRegions(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.CountRegions, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.CountRegions, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM region WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                        return -1;
                    return count;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.CountRegions, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Returns the number of checkpoints corresponding to the given country, 
        // or throws CountryDaoException in case of an error.
        public int CountCPs(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.CountCPs, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.CountCPs, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = "SELECT COUNT(*) AS count FROM cp WHERE idcountry=@idCountry;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
                    object result = command.ExecuteScalar();
                    int count;
                    if (!int.TryParse(result.ToString(), out count))
                        return -1;
                    return count;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.CountCPs, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Check whether there is a country in the DB with the given name. Returns true if there is.
        // Throws CountryDaoException in case of an error.
        public bool IsDuplicateName(string countryName)
        {
            string commandText = "SELECT COUNT(*) FROM country WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", countryName);
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
                    throw new CountryDaoException(ActivityType.CheckDuplicateName, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Checks whether the given country can be deleted.
        // Returns false in case of an error.
        // Throws CountryDaoException if there is no DB connection.
        // Deletable only if no region, CP or hike belongs to it.
        private bool IsDeletable(int idCountry)
        {
            try
            {
                if (UpdateHikeCount(idCountry) != 0)
                    return false;
                if (CountRegions(idCountry) != 0)
                    return false;
                if (CountCPs(idCountry) != 0)
                    return false;
                return true;
            }
            catch (CountryDaoException ex)
            {
                if (ex.Error == ErrorType.NoDBConnection)
                    throw;
                return false;
            }
        }

        // Deletes the given country from DB, or throws CountryDaoException in case of an error.
        public bool DeleteCountry(int idCountry)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.Delete, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.Delete, ErrorType.NoDBConnection, string.Empty);
            }
            if (!IsDeletable(idCountry))
                throw new CountryDaoException(ActivityType.Delete, ErrorType.NotDeletable, string.Empty);
            string commandText = "DELETE FROM country WHERE idcountry=@idcountry";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    command.Parameters.AddWithValue("@idcountry", idCountry);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.Delete, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Saves country data to DB, or throws CountryDaoException in case of an error.
        public bool SaveCountry (Country country)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.Save, ErrorType.NoDBConnection, string.Empty);
            }
            if (IsDuplicateName(country.Name))
            {
                throw new CountryDaoException(ActivityType.Save, ErrorType.DuplicateName, string.Empty);
            }
            string commandText = "INSERT INTO country (NAME, HIKECOUNT, DESCRIPTION) VALUES (@name, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.Name);
                command.Parameters.AddWithValue("@description", country.Description);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.Save, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Returns the data of the country with given id, or throws CountryDaoException in case of an error.
        public Country GetCountryData(int idCountry)
        {
            if (idCountry <= 0)
            {
                throw new CountryDaoException(ActivityType.GetData, ErrorType.InvalidArgument,
                    "idCountry parameter should be positive.");
            }
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.GetData, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.GetData, ErrorType.NoDBConnection, string.Empty);
            }

            string commandText = "SELECT name, description, hikecount FROM country WHERE IDCOUNTRY=@id;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@id", idCountry);
                DataTable table = new DataTable();
                try
                {
                    adapter.Fill(table);
                    DataRow row = table.Rows[0];
                    int count;
                    if (!int.TryParse(row["hikecount"].ToString(), out count))
                    {
                        throw new CountryDaoException(ActivityType.GetData, ErrorType.DBError,
                            "'hikecount' value should be an integer.");
                    }
                    Country countryData = new Country(idCountry, count, 
                        row["name"].ToString(), row["description"].ToString());
                    return countryData;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.GetData, ErrorType.DBError, ex.Message);
                }
            }
        }

        // Updates the DB with data in the country object, or throws CountryDaoException in case of an error.
        public bool UpdateCountry(Country country)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.Update, ErrorType.NoDBConnection, string.Empty);
            }
            string commandText = 
                "UPDATE country SET NAME=@name, DESCRIPTION=@description WHERE IDCOUNTRY=@id;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", country.Name);
                command.Parameters.AddWithValue("@description", country.Description);
                command.Parameters.AddWithValue("@id", country.ID);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new CountryDaoException(ActivityType.Update, ErrorType.DBError, ex.Message);
                }
            }
        }

        /* 
        public DataTable SearchCountry(CountryTemplate template)
        {
            if (sqlConnection == null)
            {
                throw new CountryDaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new CountryDaoException(ActivityType.Search, ErrorType.NoDBConnection, string.Empty);
            }
        }
        */
    }
}
