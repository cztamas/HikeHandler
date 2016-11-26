using System.Collections.Generic;
using HikeHandler.ModelObjects;

namespace HikeHandler.Interfaces
{
    public interface IDAOManager
    {
        bool DeleteCountry(int countryID);
        bool DeleteCP(CPForView cp);
        bool DeleteHike(HikeForView hike);
        bool DeleteRegion(HikeRegionForView region);
        List<NameAndID> GetAllCountryNames();
        List<NameAndID> GetAllCPs();
        List<NameAndID> GetAllCPsOfRegion(int regionID);
        List<NameAndID> GetAllRegionsOfCountry(int countryID);
        BaseFormSummary GetBaseFormSummary();
        List<NameAndID> GetCPsFromList(List<int> cpIDList);
        List<NameAndID> GetCPTypes();
        List<NameAndID> GetHikeTypes();
        bool SaveCountry(CountryForSave country);
        bool SaveCP(CPForSave cp);
        bool SaveHike(HikeForSave hike);
        bool SaveRegion(HikeRegionForSave region);
        CountryForView SearchCountry(int countryID);
        List<CountryForView> SearchCountry(CountryForSearch country);
        CPForView SearchCP(int cpID);
        List<CPForView> SearchCP(CPForSearch cp);
        HikeForView SearchHike(int hikeID);
        List<HikeForView> SearchHike(HikeForSearch hike, bool anyCPOrder);
        HikeRegionForView SearchRegion(int regionID);
        List<HikeRegionForView> SearchRegion(HikeRegionForSearch region);
        bool UpdateCountry(CountryForUpdate country);
        bool UpdateCP(CPForUpdate cp);
        bool UpdateHike(HikeForUpdate hike);
        bool UpdateRegion(HikeRegionForUpdate region);
    }
}