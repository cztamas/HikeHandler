using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Data_Containers
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

    public struct IntInterval
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public IntInterval(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public string SqlSnippet(string variable)
        {
            return "(" + Min + "<=" + variable + ") AND (" + variable + "<=" + Max + ")";
        }
    }

    public class IntPile
    {
        private IntInterval[] intervals;

        public IntPile(IntInterval[] intervalList)
        {
            intervals = intervalList;
        }

        public string SqlSearchCondition(string variable)
        {
            string condition = String.Empty;
            foreach (IntInterval interval in intervals)
            {
                if (condition != String.Empty)
                    condition += " OR ";
                string item = "(" + interval.SqlSnippet(variable) + ")";
                condition += item;
            }
            return condition;
        }
    }
}
