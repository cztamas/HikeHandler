namespace HikeHandler.ModelObjects
{
    public class IntInterval
    {
        private int min;
        private int max;

        public IntInterval(int minValue, int maxValue)
        {
            min = minValue;
            max = maxValue;
        }

        public IntInterval()
        {
            min = 0;
            max = 0;
        }

        public string SqlSnippet(string variable)
        {
            if (max > 0)
                return "(" + variable + " BETWEEN " + min + " AND " + max + ")";
            if (max == 0 && min == 0)
                return "(" + variable + "=0" + ")";
            return "(" + variable + " >= " + min + ")";
        }
    }
}
