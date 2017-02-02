using System.Collections.Generic;

namespace HikeHandler.ModelObjects
{
    public class CPForSave
    {
        public List<int> CountryID { get; set; }
        public List<int> RegionID { get; set; }
        public string Name { get; set; }
        public CPType TypeOfCP { get; set; }
        public string Description { get; set; }

        public CPForSave(List<int> countryID, List<int> regionID, string name, CPType typeOfCP, string description)
        {
            CountryID = countryID;
            RegionID = regionID;
            Name = name;
            Description = description;
            TypeOfCP = typeOfCP;
        }
    }
}
