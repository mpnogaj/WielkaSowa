using System;
using System.Linq;
using Avalonia.Data;

namespace WielkaSowa.Helpers
{
    public static class Validator
    {
        #region Const values
        public const string InvalidType = "Nieprawidłowa wartość.";
        public const string OutOfRange = "Wartość poza zakresem.";
        #endregion
        
        #region Extension methods

        #endregion

        #region Validation methods

        public static void ValidateNumber(string s)
        {
            if (!s.IsNumber())
                throw new DataValidationException(InvalidType);
        }

        public static void ValidateRealNumber(string s)
        {
            if (!s.IsRealNumber())
                throw new DataValidationException(InvalidType);
        }
        
        public static void ValidateRange(int x, Pair<double, double> r)
        { 
            ValidateRange((double)x, r);
        }
        
        public static void ValidateRange(double x, Pair<double, double> r)
        {
            if (!x.IsInRange(r.First, r.Second))
                throw new DataValidationException(OutOfRange);
        }
        #endregion
    }
}