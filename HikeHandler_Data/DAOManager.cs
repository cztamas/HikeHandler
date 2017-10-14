using HikeHandler.DAOs;
using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HikeHandler.ServiceLayer
{
    public class DAOManager : IDAOManager
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
                countryDao.RecalculateCountryData();
                cpDao.RecalculateHikeCounts();
                regionDao.RecalculateRegionData();
                hikeDao.RecalculatePositions();
        }

        // Returns the summary of DB content to be shown on the BaseForm.
        public BaseFormSummary GetBaseFormSummary()
        {
            int countries = countryDao.GetCountOfCountries();
            int regions = regionDao.GetCountOfRegions();
            int cps = cpDao.GetCountOfCPs();
            int hikes = hikeDao.GetCountOfHikes(true);
            BaseFormSummary summary = new BaseFormSummary(countries, regions, cps, hikes);
            return summary;
        }

        #region Country Methods

        public bool SaveCountry(CountryForSave country)
        {
            if (countryDao.IsDuplicateName(country.Name))
            {
                throw new DuplicateItemNameException();
            }
            countryDao.SaveCountry(country);
            return true;
        }

        public List<NameAndID> GetAllCountryNames()
        {
            return countryDao.GetAllCountryNames();
        }

        public List<CountryForView> SearchCountry(CountryForSearch country)
        {
            return countryDao.SearchCountry(country);
        }

        public CountryForView SearchCountry(int countryID)
        {
            return countryDao.GetCountryByID(countryID);
        }

        public bool UpdateCountry(CountryForUpdate country)
        {
            if (country.NewName != country.OldName)
            {
                if (countryDao.IsDuplicateName(country.NewName))
                {
                    throw new DuplicateItemNameException();
                }
            }
            countryDao.UpdateCountry(country);
            return true;
        }

        public bool DeleteCountry(int countryID)
        {
            if (!countryDao.IsDeletable(countryID))
            {
                throw new NotDeletableException();
            }
            countryDao.DeleteCountry(countryID);
            return true;
        }

        #endregion

        #region HikeRegion Methods

        // Returns in a datatable the names and ids of every region of the given country.
        public List<NameAndID> GetAllRegionsOfCountry(int countryID)
        {
                return regionDao.GetRegionsOfCountry(countryID);
        }

        public List<NameAndID> GetAllRegions()
        {
            return regionDao.GetRegionNames();
        }

        public bool SaveRegion(HikeRegionForSave region)
        {
            if (regionDao.IsDuplicateName(region.Name))
            {
                throw new DuplicateItemNameException();
            }
            regionDao.SaveRegion(region);
            countryDao.UpdateRegionCount(region.CountryID);
            return true;
        }

        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch region)
        {
            return regionDao.SearchRegion(region);
        }

        public HikeRegionForView SearchRegion(int regionID)
        {
            return regionDao.GetRegionData(regionID);
        }

        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            if (region.NewName != region.OldName)
            {
                if (regionDao.IsDuplicateName(region.NewName))
                {
                    throw new DuplicateItemNameException();
                }
            }
            regionDao.UpdateRegion(region);
            return true;
        }

        public bool DeleteRegion(HikeRegionForView region)
        {
            if (!regionDao.IsDeletable(region.RegionID))
            {
                throw new NotDeletableException();
            }
            regionDao.DeleteRegion(region.RegionID);
            countryDao.UpdateRegionCount(region.CountryID);
            return true;
        }

        #endregion

        #region CP Methods

        public List<NameAndID> GetCPTypes()
        {
            return cpDao.GetCPTypes();
        }

        // Returns in a list the names and ids of every cp of the given region.
        public List<NameAndID> GetAllCPsOfRegion(int regionID)
        {
            return cpDao.GetCPNameTable(regionID);
        }

        public List<NameAndID> GetAllCPs()
        {
            return cpDao.GetCPNameTable();
        }

        public List<NameAndID> GetCPsFromList(List<int> cpIDList)
        {
            return cpDao.GetCPNameTable(cpIDList);
        }

        public bool SaveCP(CPForSave cp)
        {
            if (cpDao.IsDuplicateName(cp.Name))
            {
                throw new DuplicateItemNameException();
            }
            cpDao.SaveCP(cp);
            countryDao.UpdateCPCount(cp.CountryID);
            regionDao.UpdateCPCount(cp.RegionID);
            return true;
        }

        public List<CPForView> SearchCP(CPForSearch cp)
        {
            return cpDao.SearchCP(cp);
        }

        public CPForView SearchCP(int cpID)
        {
            return cpDao.GetCPData(cpID);
        }

        public bool UpdateCP(CPForUpdate cp)
        {
            if (cp.NewName != cp.OldName)
            {
                if (cpDao.IsDuplicateName(cp.NewName))
                {
                    throw new DuplicateItemNameException();
                }
            }
            cpDao.UpdateCP(cp);
            return true;
        }

        public bool DeleteCP(CPForView cp)
        {
            if (!cpDao.IsDeletable(cp.CPID))
            {
                throw new NotDeletableException();
            }
            cpDao.DeleteCP(cp.CPID);
            regionDao.UpdateCPCount(cp.RegionID);
            countryDao.UpdateCPCount(cp.CountryID);
            return true;
        }

        #endregion

        #region Hike Methods

        public List<NameAndID> GetHikeTypes()
        {
            return hikeDao.GetHikeTypes();
        }

        public bool SaveHike(HikeForSave hike)
        {
            if (hikeDao.IsDuplicateDate(hike.HikeDate))
            {
                throw new DuplicateDateException();
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

        public List<HikeForView> SearchHike(HikeForSearch hike)
        {
            return hikeDao.SearchHike(hike);
        }

        public HikeForView SearchHike(int hikeID)
        {
            return hikeDao.GetHikeData(hikeID);
        }

        public bool UpdateHike(HikeForUpdate hike)
        {
            bool dateChanged = (hike.OldHikeDate != hike.NewHikeDate);
            bool typeChanged = (hike.OldHikeType != hike.NewHikeType);
            if (dateChanged && hikeDao.IsDuplicateDate(hike.NewHikeDate))
            {
                throw new DuplicateDateException();
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

        public bool DeleteHike(HikeForView hike)
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
        
        #endregion
    }
}
