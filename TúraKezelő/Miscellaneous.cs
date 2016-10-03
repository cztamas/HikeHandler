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
        public int Min { get; set; }
        public int Max { get; set; }

        public IntInterval(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public IntInterval()
        {
            Min = 0;
            Max = 0;
        }

        public string SqlSnippet(string variable)
        {
            if (Max > 0)
                return "(" + variable + "BETWEEN " + Min + " AND " + Max + ")";
            return "(" + variable + " >= " + Min + ")";
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
}
