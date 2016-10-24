using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace HikeHandler.Data_Containers
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HikeCount { get; set; }
        public string Description { get; set; }

        public Country(int countryID)
        {
            ID = countryID;
        }

        public Country(int countryID, int hikeNumber, string countryName, string countryDescription)
        {
            ID = countryID;
            HikeCount = hikeNumber;
            Name = countryName;
            Description = countryDescription;
        }

        public Country(string countryName, string countryDescription)
        {
            Name = countryName;
            Description = countryDescription;
            HikeCount = 0;
        }
    }
}
