using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using HikeHandler.ModelObjects;
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

        public DataTable SearchRegion(HikeRegionForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
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
                adapter.Fill(resultTable);
                return resultTable;
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
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE idregion=" + regionID + ";";
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

        // Checks whether the given region can be deleted.
        // Deletable only if no CP or hike belongs to it.
        private bool IsDeletable(int regionID)
        {
            HikeRegionForView region = GetRegionData(regionID);
            if (region.HikeCount > 0 || region.CPCount > 0)
                return false;
            else
                return true;
        }

        public HikeRegionForView GetRegionData(int regionID)
        {
            throw new NotImplementedException();
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
            string commandText = "SELECT COUNT(*) FROM country WHERE name=@name;";
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
    }
}
