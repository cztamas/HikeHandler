using System;
using System.Collections.Generic;
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

        public List<CPForView> SearchCP(CPForSearch template)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            string commandText = @"SELECT cp.idcp, cp.name, cp.type, cp.hikecount, cp.idregion, cp.idcountry, r.name AS regionname, 
c.name AS countryname, cp.description FROM cp, region r, country c WHERE cp.idregion=r.idregion AND cp.idcountry=c.idcountry
AND cp.name LIKE @name AND c.name LIKE @countryName AND r.name LIKE @regionName";
            if (template.CPID != null)
                commandText += " AND cp.idcp=" + template.CPID;
            if (template.IDRegion != null)
                commandText += " AND r.idregion=" + template.IDRegion;
            if (template.IDCountry != null)
                commandText += " AND c.idcountry=" + template.IDCountry;
            if (template.HikeCount != null)
            {
                if (template.HikeCount.Count > 0)
                    commandText += " AND " + template.HikeCount.SqlSearchCondition("cp.hikecount");
            }
            if (template.TypeOfCP != null)
                commandText += " AND cp.type=@type";
            commandText += " ORDER BY cp.name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@name", "%" + template.Name + "%");
                command.Parameters.AddWithValue("@countryName", "%" + template.CountryName + "%");
                command.Parameters.AddWithValue("@regionName", "%" + template.RegionName + "%");
                if (template.TypeOfCP != null)
                {
                    command.Parameters.AddWithValue("@type", template.TypeOfCP.ToString());
                }
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<CPForView> resultList = new List<CPForView>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int cpID = reader.GetInt32("idcp");
                            int countryID = reader.GetInt32("idcountry");
                            int regionID = reader.GetInt32("idregion");
                            int hikeCount = reader.GetInt32("hikecount");
                            string name = reader.GetString("name");
                            string countryName = reader.GetString("countryname");
                            string regionName = reader.GetString("regionname");
                            string description = reader.GetString("description");
                            CPType typeOfCP;
                            if (!Enum.TryParse(reader.GetString("type"), out typeOfCP))
                            {
                                throw new DBErrorException("Invalid CheckPoint type found.");
                            }
                            resultList.Add(new CPForView(
                                cpID, countryID, regionID, name, countryName, regionName, typeOfCP, hikeCount, description));
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

        // Returns the data of the cp with the given id.
        public CPForView GetCPData(int cpID)
        {
            if (cpID <= 0)
            {
                throw new ArgumentException("cpID parameter should be positive.", "cpID");
            }
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }

            string commandText = @"SELECT cp.idregion, cp.idcountry, cp.name, cp.type, cp.hikecount, cp.description, r.name AS regionname, 
c.name AS countryname FROM cp, region r, country c WHERE cp.idregion=r.idregion AND cp.idcountry=c.idcountry AND cp.idcp=@idcp;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@idcp", cpID);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    return null;
                }
                if (table.Rows.Count > 1)
                {
                    throw new DBErrorException("More than one checkpoint found with the given id.");
                }
                DataRow row = table.Rows[0];

                string name;
                string countryName;
                string regionName;
                string description;
                int countryID;
                int regionID;
                int hikeCount;
                CPType typeOfCP;

                if (!int.TryParse(row["hikecount"].ToString(), out hikeCount))
                {
                    throw new DBErrorException("'cp.hikecount' should be an integer.");
                }
                if (!int.TryParse(row["idregion"].ToString(), out regionID))
                {
                    throw new DBErrorException("'cp.idregion' should be an integer.");
                }
                if (!int.TryParse(row["idcountry"].ToString(), out countryID))
                {
                    throw new DBErrorException("'cp.idcountry' should be an integer.");
                }
                if (!Enum.TryParse<CPType>(row["type"].ToString(), out typeOfCP))
                {
                    throw new DBErrorException("'cp.cptype' value not valid.");
                }
                name = row["name"].ToString();
                description = row["description"].ToString();
                countryName = row["countryname"].ToString();
                regionName = row["regionname"].ToString();

                return new CPForView(cpID, countryID, regionID, name, countryName, regionName, typeOfCP, hikeCount, description);
            }
        }

        // Returns in a datatable the names and ids of every cp of the given region.
        public List<NameAndID> GetCPNameTable(int regionID)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            List<NameAndID> result = new List<NameAndID>();
            string commandText = "SELECT name, idcp FROM cp WHERE idregion=@idregion ORDER BY name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                command.Parameters.AddWithValue("@idregion", regionID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("idcp");
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

        // Returns in a datatable the names and ids of every cp in the DB.
        public List<NameAndID> GetCPNameTable()
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            List<NameAndID> result = new List<NameAndID>();
            string commandText = "SELECT name, idcp FROM cp ORDER BY name ASC;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("idcp");
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

        // Returns in a datatable the names and ids of the cps specified in the given list.
        public List<NameAndID> GetCPNameTable(List<int> cpIDList)
        {
            if (sqlConnection == null)
            {
                throw new NoDBConnectionException();
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new NoDBConnectionException();
            }
            List<NameAndID> result = new List<NameAndID>();
            string commandText = "SELECT name, idcp FROM cp WHERE idcp=@idcp;";
            int lastID = -1;
            foreach (int item in cpIDList)
            {
                using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
                {
                    command.Parameters.AddWithValue("@idcp", item);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("idcp");
                                if (id == lastID)
                                {
                                    throw new DBErrorException("More than one cp found with the given ID.");
                                }
                                string name = reader.GetString("name");
                                result.Add(new NameAndID(name, id));
                                lastID = id;
                            }
                        }
                        else
                        {
                            throw new DBErrorException("The given cp cannot be found.");
                        }
                    }
                }
            }
            return result;
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

        // Returns the number of checkpoints in the DB.
        public int GetCountOfCPs()
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
            string commandText = "SELECT COUNT(*) FROM cp;";
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

        public List<NameAndID> GetCPTypes()
        {
            List<NameAndID> result = new List<NameAndID>();
            Array cpTypes = Enum.GetValues(typeof(CPType));
            foreach (CPType item in cpTypes)
            {
                result.Add(new NameAndID(item.ToString(), (int)item));
            }
            return result;
        }
    }
}
