using System;
using System.Data;
using MySql.Data.MySqlClient;
using HikeHandler.ModelObjects;
using HikeHandler.Exceptions;

namespace HikeHandler.DAOs
{
    public class CPDao
    {
        private MySqlConnection sqlConnection;

        public CPDao(MySqlConnection connection)
        {
            sqlConnection = connection;
        }

        public DataTable SearchCP(CPForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
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
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // Recalculates the hike count of every cp in the DB.
        // Only for correcting erroneous data in the DB.
        public void RecalculateHikeCounts()
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
            string commandText = "SELECT idcp FROM cp;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                if (!int.TryParse(row["idcp"].ToString(), out id))
                {
                    throw new DBErrorException("'idcp' value should be an integer.");
                }
                UpdateHikeCount(id);
            }
        }

        // Finds the correct hikecount, and stores it in the DB.
        // Returns the updated value of the hikecount.
        public int UpdateHikeCount(int idCP)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) AS count FROM hike WHERE cpstring LIKE '%." + idCP + ".%';";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("SELECT COUNT return value should be integer.");

                commandText = "UPDATE cp SET hikecount=@hikecount WHERE idcp=@idcp;";
                using (MySqlCommand updateCommand = new MySqlCommand(commandText, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@hikecount", count);
                    updateCommand.Parameters.AddWithValue("@idcp", idCP);
                    updateCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }

        // Checks whether the given checkpoint can be deleted.
        // Deletable only if no hike belongs to it.
        public bool IsDeletable(int cpID)
        {
            CPForView cpData = GetCPData(cpID);
            if (cpData.HikeCount > 0)
                return false;
            else
                return true;
        }

        public CPForView GetCPData(int cpID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCP(int idCP)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "DELETE FROM cp WHERE idcp=@idcp";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idcp", idCP);
                command.ExecuteNonQuery();
            }
        }

        public bool IsDuplicateName(string name)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "SELECT COUNT(*) FROM cp WHERE name=@name;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", name);
                object result = command.ExecuteScalar();
                int count;
                if (!int.TryParse(result.ToString(), out count))
                    throw new DBErrorException("'SELECT COUNT' return value should be an integer.");
                if (count == 0)
                    return false;
                if (count > 1)
                {
                    throw new DBErrorException("More than one checkpoint found with the same name.");
                }
                return true;
            }
        }

        public void UpdateCP(CPForUpdate cpData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = "UPDATE cp SET name=@name, type=@type, description=@description WHERE idcp=@idcp;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", cpData.NewName);
                command.Parameters.AddWithValue("@type", cpData.TypeOfCP.ToString());
                command.Parameters.AddWithValue("@description", cpData.Description);
                command.Parameters.AddWithValue("@idcp", cpData.CPID);
                command.ExecuteNonQuery();
            }
        }

        public void SaveCP(CPForSave cpData)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"INSERT INTO cp (name, idcountry, idregion, type, hikecount, description) 
VALUES (@name, @idcountry, @idregion, @type, 0, @description);";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", cpData.Name);
                command.Parameters.AddWithValue("@idcountry", cpData.CountryID);
                command.Parameters.AddWithValue("@idregion", cpData.RegionID);
                command.Parameters.AddWithValue("@type", cpData.TypeOfCP.ToString());
                command.Parameters.AddWithValue("@description", cpData.Description);
                command.ExecuteNonQuery();
            }
        }
    }
}
