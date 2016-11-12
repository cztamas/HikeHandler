using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using HikeHandler.DAOs;
using HikeHandler.ModelObjects;
using HikeHandler.Exceptions;

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

        // Only for correcting erroneous data in the DB.
        public void RecalculateDBData()
        {
            try
            {
                countryDao.RecalculateCountryData();
                cpDao.RecalculateHikeCounts();
                regionDao.RecalculateRegionData();
                hikeDao.RecalculatePositions();
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs lehet elérni adatbázist.", "Hiba");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        // Returns the summary of DB content to be shown on the BaseForm.
        public BaseFormSummary GetBaseFormSummary()
        {
            try
            {
                int countries = countryDao.GetCountOfCountries();
                int regions = regionDao.GetCountOfRegions();
                int cps = cpDao.GetCountOfCPs();
                int hikes = hikeDao.GetCountOfHikes();
                BaseFormSummary summary = new BaseFormSummary(countries, regions, cps, hikes);
                return summary;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
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
                countryDao.SaveCountry(country);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            return false;
        }

        public List<NameAndID> GetAllCountryNames()
        {
            List<NameAndID> result = countryDao.GetCountryNames();
            return result;

            /*catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }*/
        }

        public List<CountryForView> SearchCountry(CountryForSearch country)
        {
            List<CountryForView> resultList = countryDao.SearchCountry(country);
                return resultList;
            
            /*catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }*/
        }

        public CountryForView SearchCountry(int countryID)
        {
            try
            {
                CountryForView country = countryDao.GetCountryData(countryID);
                if (country == null)
                {
                    MessageBox.Show("Nem található a keresett ország.", "Hiba");
                }
                return country;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool UpdateCountry(CountryForUpdate country)
        {
            try
            {
                if (country.NewName != country.OldName)
                {
                    if (countryDao.IsDuplicateName(country.NewName))
                    {
                        MessageBox.Show("Már van elmentve ilyen nevű ország.");
                        return false;
                    }
                }
                countryDao.UpdateCountry(country);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        public bool DeleteCountry(int countryID)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            try
            {
                if (!countryDao.IsDeletable(countryID))
                {
                    MessageBox.Show("Csak olyan ország törölhető, amihez nem tartozik tájegység, checkpoint vagy túra.");
                    return false;
                }
                countryDao.DeleteCountry(countryID);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        #endregion

        #region HikeRegion Methods

        // Returns in a datatable the names and ids of every region of the given country.
        public List<NameAndID> GetAllRegionsOfCountry(int countryID)
        {
                List<NameAndID> regions = regionDao.GetRegionNames(countryID);
                return regions;
        }

        public bool SaveRegion(HikeRegionForSave region)
        {
            try
            {
                if (regionDao.IsDuplicateName(region.Name))
                {
                    MessageBox.Show("Már van elmentve ilyen nevű tájegység.", "Hiba");
                    return false;
                }
                regionDao.SaveRegion(region);
                countryDao.UpdateRegionCount(region.CountryID);
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

        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch region)
        {
            List<HikeRegionForView> result = regionDao.SearchRegion(region);
            return result;
        }

        public HikeRegionForView SearchRegion(int regionID)
        {
            try
            {
                HikeRegionForView region = regionDao.GetRegionData(regionID);
                if (region == null)
                {
                    MessageBox.Show("Nem található a keresett tájegység.", "Hiba");
                }
                return region;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            try
            {
                if (region.NewName != region.OldName)
                {
                    if (regionDao.IsDuplicateName(region.NewName))
                    {
                        MessageBox.Show("Már van elmentve ilyen nevű tájegység.");
                        return false;
                    }
                }
                regionDao.UpdateRegion(region);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        public bool DeleteRegion(HikeRegionForView region)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Tájegység törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            try
            {
                if (!regionDao.IsDeletable(region.RegionID))
                {
                    MessageBox.Show("Csak olyan tájegység törölhető, amihez nem tartozik checkpoint vagy túra.");
                    return false;
                }
                regionDao.DeleteRegion(region.RegionID);
                countryDao.UpdateRegionCount(region.CountryID);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        #endregion

        #region CP Methods

        public DataTable GetCPTypes()
        {
            try
            {
                return cpDao.GetCPTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        // Returns in a datatable the names and ids of every cp of the given region.
        public DataTable GetAllCPsOfRegion(int regionID)
        {
            try
            {
                DataTable table = cpDao.GetCPNameTable(regionID);
                return table;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public DataTable GetAllCPs()
        {
            try
            {
                DataTable table = cpDao.GetCPNameTable();
                return table;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public DataTable GetCPsFromList(List<int> cpIDList)
        {
            try
            {
                DataTable table = cpDao.GetCPNameTable(cpIDList);
                return table;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool SaveCP(CPForSave cp)
        {
            try
            {
                if (cpDao.IsDuplicateName(cp.Name))
                {
                    MessageBox.Show("Már van elmentve ilyen nevű checkpoint.", "Hiba");
                    return false;
                }
                cpDao.SaveCP(cp);
                countryDao.UpdateCPCount(cp.CountryID);
                regionDao.UpdateCPCount(cp.RegionID);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            return false;
        }

        public DataTable SearchCP(CPForSearch cp)
        {
            try
            {
                DataTable table = cpDao.SearchCP(cp);
                return table;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public CPForView SearchCP(int cpID)
        {
            try
            {
                CPForView cp = cpDao.GetCPData(cpID);
                if (cp == null)
                {
                    MessageBox.Show("Nem található a keresett checkpoint.", "Hiba");
                }
                return cp;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool UpdateCP(CPForUpdate cp)
        {
            try
            {
                if (cp.NewName != cp.OldName)
                {
                    if (cpDao.IsDuplicateName(cp.NewName))
                    {
                        MessageBox.Show("Már van elmentve ilyen nevű checkpoint.");
                        return false;
                    }
                }
                cpDao.UpdateCP(cp);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        public bool DeleteCP(CPForView cp)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            try
            {
                if (!cpDao.IsDeletable(cp.CPID))
                {
                    MessageBox.Show("Csak olyan checkpoint törölhető, amihez nem tartozik túra.");
                    return false;
                }
                cpDao.DeleteCP(cp.CPID);
                regionDao.UpdateCPCount(cp.RegionID);
                countryDao.UpdateCPCount(cp.CountryID);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        #endregion

        #region Hike Methods

        public DataTable GetHikeTypes()
        {
            try
            {
                return hikeDao.GetHikeTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool SaveHike(HikeForSave hike)
        {
            try
            {
                if (hikeDao.IsDuplicateDate(hike.HikeDate))
                {
                    MessageBox.Show("Ezzel a dátummal már van elmentve túra.", "Hiba");
                    return false;
                }
                hikeDao.SaveHike(hike);
                if (hike.HikeType == HikeType.túra)
                {
                    hikeDao.InsertIntoPositionList(hike.HikeDate);
                    countryDao.UpdateHikeCount(hike.CountryID);
                    regionDao.UpdateHikeCount(hike.RegionID);
                    foreach (int item in hike.CPList)
                    {
                        cpDao.UpdateHikeCount(item);
                    }
                }
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            return false;
        }

        public DataTable SearchHike(HikeForSearch hike, bool anyCPOrder)
        {
            try
            {
                DataTable table = hikeDao.SearchHike(hike, anyCPOrder);
                return table;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public HikeForView SearchHike(int hikeID)
        {
            try
            {
                HikeForView hike = hikeDao.GetHikeData(hikeID);
                if (hike == null)
                {
                    MessageBox.Show("Nem található a keresett ország.", "Hiba");
                }
                return hike;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return null;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
        }

        public bool UpdateHike(HikeForUpdate hike)
        {
            try
            {
                bool dateChanged = (hike.OldHikeDate != hike.NewHikeDate);
                bool typeChanged = (hike.OldHikeType != hike.NewHikeType);
                if (dateChanged && hikeDao.IsDuplicateDate(hike.NewHikeDate))
                {
                    MessageBox.Show("Ezzel a dátummal már van elmentve túra", "Hiba");
                }
                hikeDao.UpdateHike(hike);
                if ((typeChanged || dateChanged) && hike.OldHikeType == HikeType.túra)
                {
                    hikeDao.RemoveFromPositionList(hike.HikeID, hike.OldHikeDate);
                }
                if ((typeChanged || dateChanged) && hike.NewHikeType == HikeType.túra)
                {
                    hikeDao.InsertIntoPositionList(hike.NewHikeDate);
                }
                if (typeChanged)
                {
                    countryDao.UpdateHikeCount(hike.CountryID);
                    regionDao.UpdateHikeCount(hike.RegionID);
                }
                foreach (int item in hike.OldCPList)
                    cpDao.UpdateHikeCount(item);
                foreach (int item in hike.NewCPList)
                    cpDao.UpdateHikeCount(item);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        public bool DeleteHike(HikeForView hike)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return false;

            try
            {
                hikeDao.DeleteHike(hike);
                hikeDao.MovePositions(hike.HikeDate, false);
                foreach (int item in hike.CPList)
                {
                    cpDao.UpdateHikeCount(item);
                }
                regionDao.UpdateHikeCount(hike.RegionID);
                countryDao.UpdateHikeCount(hike.CountryID);
                return true;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return false;
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
        }

        #endregion
    }
}
