using Newtonsoft.Json;
using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public struct CPForView
    {
        public int cpID, hikeCount, cpType;
        public string name, description, cpTypeName;
        public List<int> countryIDs, regionIDs;
        public List<string> countryNames, regionNames;

        [JsonConstructor]
        public CPForView(int cpID, string name, List<int> countryIDs, List<int> regionIDs, List<string> countryNames, 
            List<string> regionNames, int hikeCount, string description, int cpType, string cpTypeName)
        {
            this.cpID = cpID;
            this.countryIDs = countryIDs;
            this.regionIDs = regionIDs;
            this.name = name;
            this.countryNames = countryNames;
            this.regionNames = regionNames;
            this.cpType = cpType;
            this.cpTypeName = cpTypeName;
            this.hikeCount = hikeCount;
            this.description = description;
        }
    }
}
