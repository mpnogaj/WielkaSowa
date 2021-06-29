using System;
using System.Linq;

namespace WielkaSowa.Helpers
{
    public static class ExtensionMethods
    {
        public static bool IsNumber(this string a)
        {
            // Convert to 0
            if (a.Length == 0) return true;
            return a.All(char.IsDigit) && (a.Length == 1 || a[0] != '0');
        }

        public static bool IsRealNumber(this string a)
        {
            // Convert to 0.0
            if (a.Length == 0) return true;
            return !a.Any((x) => !char.IsDigit(x) && (x != '.' && x != ',')) && a[0] != '0';
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
            if (!s.IsRealNumber()) return 0;
            try
            {
                return Convert.ToDouble(s.Replace('.', ','));
            }
            catch
            {
                return 0;
            }
        }

        public static int ToInt(this string s)
        {
            return (int) s.ToDouble();
        }
    }
}