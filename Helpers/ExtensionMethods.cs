using System;
using System.Linq;

namespace WielkaSowa.Helpers
{
    public static class ExtensionMethods
    {
        public static bool IsNumber(this string a)
        {
            return a.All(char.IsDigit);
        }

        public static bool IsRealNumber(this string a)
        {
            return !a.Any((x) => !char.IsDigit(x) && (x != '.' && x != ','));
        }

        public static bool IsInRange(this int x, double l, double r)
        {
            return x <= r && x >= l;
        }
        
        public static bool IsInRange(this double x, double l, double r)
        {
            return x <= r && x >= l;
        }

        public static double ToDouble(this string s)
        {
            if (s == string.Empty) return 0;
            try
            {
                return Convert.ToDouble(s.Replace('.', ','));
            }
            catch
            {
                return 0;
            }
        }
    }
}