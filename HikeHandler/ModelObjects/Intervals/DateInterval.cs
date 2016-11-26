using System;

namespace HikeHandler.ModelObjects
{
    public class DateInterval
    {
        private DateTime? min;
        private DateTime? max;

        public DateInterval(DateTime begin, DateTime end)
        {
            min = begin;
            max = end;
        }

        public DateInterval(DateTime date, bool isMax)
        {
            if (isMax)
            {
                min = null;
                max = date;
            }
            if (!isMax)
            {
                min = date;
                max = null;
            }
        }

        public string SqlSnippet(string variable)
        {
            if (min != null && max != null)
                return "('" + ((DateTime)min).ToString("yyyy-MM-dd") + "' <= " + variable + " AND "
                    + variable + " <= '" + ((DateTime)max).ToString("yyyy-MM-dd") + "')";
            if (min == null && max != null)
                return variable + " <= '" + ((DateTime)max).ToString("yyyy-MM-dd") + "' ";
            if (min != null && max == null)
                return variable + " >= '" + ((DateTime)min).ToString("yyyy-MM-dd") + "' ";
            return string.Empty;
        }
    }
}
