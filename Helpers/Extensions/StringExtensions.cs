using System;

namespace WielkaSowa.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string s)
        {
            // Convert to 0
            if (s.Length == 0) return true;
            try
            {
                Convert.ToInt32(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsRealNumber(this string s)
        {
            // Convert to 0.0
            if (s.Length == 0) return true;
            try
            {
                Convert.ToDouble(s.Replace('.', ','));
                return true;
            }
            catch
            {
                return false;
            }
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
            return (int)s.ToDouble();
        }
    }
}
