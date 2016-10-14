using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler
{
    public struct LoginData
    {
        public string username;
        public string password;

        public LoginData(string user, string pwd)
        {
            username = user;
            password = pwd;
        }
    }

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

    public class IntPile
    {
        private List<IntInterval> intervals;

        public IntPile()
        {
            intervals = new List<IntInterval>();
        }

        public IntPile(List<IntInterval> intervalList)
        {
            intervals = intervalList;
        }

        public void Add(IntInterval interval)
        {
            intervals.Add(interval);
        }

        public int Count()
        {
            if (intervals == null)
                return 0;
            return intervals.Count;
        }

        public string SqlSearchCondition(string variable)
        {
            string condition = String.Empty;
            foreach (IntInterval interval in intervals)
            {
                if (condition != String.Empty)
                    condition += " OR ";                
                condition += interval.SqlSnippet(variable);
            }
            if (condition == String.Empty)
                return String.Empty;
            return "( " + condition + " )";
        }
    }

    public class DatePile
    {
        private List<DateInterval> intervals;
        private List<int> months;
        private List<int> weekdays;

        public DatePile(List<DateInterval> dateIntervals)
        {
            intervals = dateIntervals;
        }

        public DatePile()
        {
            intervals = new List<DateInterval>();
        }

        public void Add(DateInterval dateInterval)
        {
            intervals.Add(dateInterval);
        }

        public string SqlSearchCondition(string variable)
        {
            if (intervals == null)
                return string.Empty;
            string condition = string.Empty;
            foreach (DateInterval interval in intervals)
            {
                if (interval.SqlSnippet(variable)!=string.Empty)
                {
                    if (condition != String.Empty)
                        condition += " OR ";
                    condition += interval.SqlSnippet(variable);
                }
            }
            if (condition == string.Empty)
                return string.Empty;
            return "(" + condition + ")";
        }
    }

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
                return "(" + min.ToString() + " <= " + variable + " AND " + variable + " <= " + max.ToString() + ")";
            if (min == null && max != null)
                return variable + " <= " + max.ToString();
            if (min != null && max == null) 
                return variable + " >= " + min.ToString();
            return string.Empty;
        }
    }

    public enum CPType
    {
        település, turistaház, tereppont, egyéb
    }

    public enum HikeType
    {
        túra=0, séta
    }    
}
