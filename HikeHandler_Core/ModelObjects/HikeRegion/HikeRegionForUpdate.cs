using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForUpdate
    {
        public int RegionID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public HikeRegionForUpdate(int regionID, string oldName, string newName, string description)
        {
            RegionID = regionID;
            OldName = oldName;
            NewName = newName;
            Description = description;
        }
    }
}
