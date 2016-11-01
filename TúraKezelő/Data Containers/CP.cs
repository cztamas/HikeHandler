using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
{
    public class CP
    {
        public int CPID { get; set; }
        public int IDCountry { get; set; }
        public int IDRegion { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public CPType? TypeOfCP { get; set; }
        public string Description { get; set; }
        public int HikeCount { get; set; }

        public CP() { }

        public CP(int idCP)
        {
            CPID = idCP;
        }
    }
}
