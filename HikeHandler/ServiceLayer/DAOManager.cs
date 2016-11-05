using HikeHandler.DAOs;
using HikeHandler.ModelObjects;
using HikeHandler.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HikeHandler.ServiceLayer
{
    public class DAOManager
    {
        private CountryDao countryDao;
        private RegionDao regionDao;
        private CPDao cpDao;
        private HikeDao hikeDao;

        public DAOManager(MySqlConnection connection)
        {
            countryDao = new CountryDao(connection);
            regionDao = new RegionDao(connection);
            cpDao = new CPDao(connection);
            hikeDao = new HikeDao(connection);
        }

        #region Country Methods

        public bool SaveCountry(CountryForSave country)
        {
            try
            {
                if (countryDao.IsDuplicateName(country.Name))
                {
                    MessageBox.Show("Már van elmentve ilyen nevű ország.", "Hiba");
                    return false;
                }
                if (countryDao.SaveCountry(country))
                    return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            return false;
        }

        public DataTable SearchCountry(CountryForSearch country)
        {
            throw new NotImplementedException();
        }

        public CountryForView SearchCountry(int countryID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCountry(CountryForUpdate country)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCountry(int countryID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region HikeRegion Methods

        public bool SaveRegion(HikeRegionForSave region)
        {
            throw new NotImplementedException();
        }

        public DataTable SearchRegion(HikeRegionForSearch region)
        {
            throw new NotImplementedException();
        }

        public HikeRegionForView SearchRegion(int regionID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRegion(int regionID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CP Methods

        public bool SaveCP(CPForSave cp)
        {
            throw new NotImplementedException();
        }

        public DataTable SearchCP(CPForSearch cp)
        {
            throw new NotImplementedException();
        }

        public CPForView SearchCP(int cpID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCP(CPForUpdate cp)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCP(int cpID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Hike Methods

        public bool SaveHike(HikeForSave region)
        {
            throw new NotImplementedException();
        }

        public DataTable SearchHike(HikeForSearch region)
        {
            throw new NotImplementedException();
        }

        public HikeForView SearchHike(int hikeID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHike(HikeForUpdate hike)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHike(int hikeID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
