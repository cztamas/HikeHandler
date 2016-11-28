using Newtonsoft.Json;

namespace HikeHandler.ModelObjects
{
    public class IntInterval
    {
        public int Min { get; set; }
        public int Max { get; set; }

        [JsonConstructor]
        public IntInterval(int min, int max)
        {
            Min = min;
            Max = max;
        }

        /*public IntInterval()
        {
            Min = 0;
            Max = 0;
        }*/

        public string SqlSnippet(string variable)
        {
            if (Max > 0)
                return "(" + variable + " BETWEEN " + Min + " AND " + Max + ")";
            if (Max == 0 && Min == 0)
                return "(" + variable + " = 0" + ")";
            return "(" + variable + " >= " + Min + ")";
        }
    }
}
