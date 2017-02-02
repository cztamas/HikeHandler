using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class CPForView
    {
        public int CPID { get; set; }
        public List<int> CountryID { get; set; }
        public List<int> RegionID { get; set; }
        public string Name { get; set; }
        public List<string> CountryNames { get; set; }
        public List<string> RegionNames { get; set; }
        public CPType TypeOfCP { get; set; }
        public string Description { get; set; }
        public int HikeCount { get; set; }

        public CPForView(int idCP)
        {
            CPID = idCP;
        }

        [JsonConstructor]
        public CPForView(int cpID, List<int> countryID, List<int> regionID, string name, List<string> countryNames, 
            List<string> regionNames, CPType typeOfCP, int hikeCount, string description)
        {
            CPID = cpID;
            CountryID = countryID;
            RegionID = regionID;
            Name = name;
            CountryNames = countryNames;
            RegionNames = regionNames;
            TypeOfCP = typeOfCP;
            HikeCount = hikeCount;
            Description = description;
        }
    }
}
