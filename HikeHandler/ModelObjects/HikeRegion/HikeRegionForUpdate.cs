using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.ModelObjects
{
    public class HikeRegionForUpdate
    {
        public int RegionID
        {
            get
            {
                return RegionID;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("RegionID has to be positive.", "countryID");
                RegionID = value;
            }
        }
        public string OldName
        {
            get
            {
                return OldName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("name", "Region name cannot be whitespace or empty");
                OldName = value;
            }
        }
        public string NewName
        {
            get
            {
                return NewName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("name", "Region name cannot be whitespace or empty");
                NewName = value;
            }
        }
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
