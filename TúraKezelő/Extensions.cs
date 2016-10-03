using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HikeHandler
{
    public static class Extensions
    {
         public static bool IsIntPile(this string text)
        {
            text = Regex.Replace(text, @"\s+", "");
            return Regex.IsMatch(text, @"^(\d*-?\d*,+)*(\d*-?\d*)?$");
        }

        public static bool IsIntInterval(this string text)
        {
            text = Regex.Replace(text, @"\s+", "");
            return Regex.IsMatch(text, @"^\d*-?\d*$");
        }

        public static IntInterval ToIntInterval(this string text)
        {
            if (!text.IsIntInterval())
                return null;
            text = Regex.Replace(text, @"\s+", "");
            char[] separator = new char[] { '-' };
            string[] limits = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (limits.Length == 0) 
            {
                return null;
            }
            if (limits.Length == 1) 
            {
                int a;
                int.TryParse(limits[0], out a);
                IntInterval interval = new IntInterval(a, 0);
                return interval;
            }
            if (limits.Length == 2)
            {
                int a, b;
                int.TryParse(limits[0], out a);
                int.TryParse(limits[1], out b);
                IntInterval interval = new IntInterval(a, b);
                return interval;
            }
            return null;
        }

        public static IntPile ToIntPile(this string text)
        {
            if (!text.IsIntPile())
                return null;
            IntPile pile = new IntPile();
            text = Regex.Replace(text, @"\s+", "");
            char[] separator = new char[] { ',' };
            string[] intervalStrings = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in intervalStrings)
            {
                IntInterval interval = item.ToIntInterval();
                if (interval != null)
                    pile.Add(item.ToIntInterval());
            }
            return pile;
        }
    }
}
