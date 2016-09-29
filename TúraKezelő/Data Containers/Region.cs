using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TúraKezelő.Data_Containers
{
    public class Region
    {
        private int id;
        private int countryId;
        private int hikeCount;
        private string name;
        private string description;

        public Region(int idOfCountry, string nameOfRegion, string descriptionOfRegion)
        {
            countryId = idOfCountry;
            name = nameOfRegion;
            description = descriptionOfRegion;
        }
    }
}
