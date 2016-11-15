using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForUpdate
    {
        public int RegionID { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }

        public HikeRegionForUpdate(int regionID, string oldName, string newName, string description)
        {
            RegionID = regionID;
            OldName = oldName;
            NewName = newName;
            Description = description;
        }
    }
}
