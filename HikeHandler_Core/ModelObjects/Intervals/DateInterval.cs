using Newtonsoft.Json;
using System;

namespace HikeHandler.ModelObjects
{
    public class DateInterval
    {
        public DateTime? Begin;
        public DateTime? End;

        [JsonConstructor]
        public DateInterval(DateTime? begin = null, DateTime? end = null)
        {
            Begin = begin;
            End = end;
        }
        
        public string SqlSnippet(string variable)
        {
            if (Begin != null && End != null)
                return "('" + ((DateTime)Begin).ToString("yyyy-MM-dd") + "' <= " + variable + " AND "
                    + variable + " <= '" + ((DateTime)End).ToString("yyyy-MM-dd") + "')";
            if (Begin == null && End != null)
                return variable + " <= '" + ((DateTime)End).ToString("yyyy-MM-dd") + "' ";
            if (Begin != null && End == null)
                return variable + " >= '" + ((DateTime)Begin).ToString("yyyy-MM-dd") + "' ";
            return string.Empty;
        }
    }
}
