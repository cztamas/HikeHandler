using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForView
    {
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public int HikeCount { get; set; }
        public int CPCount { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }

        public HikeRegionForView(int regionID)
        {
            RegionID = regionID;
        }

        [JsonConstructor]
        public HikeRegionForView(int regionID, int countryID, string name, string countryName, int hikeCount, int cpCount, string description)
        {
            RegionID = regionID;
            CountryID = countryID;
            Name = name;
            CountryName = countryName;
            HikeCount = hikeCount;
            CPCount = cpCount;
            Description = description;
        }
    }
}
