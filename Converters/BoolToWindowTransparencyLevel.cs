using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace WielkaSowa.Converters
{
    public class BoolToWindowTransparencyLevel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(WindowTransparencyLevel))
            {
                return ToWindowTransparencyLevel((bool) value);
            }

            return ToBoolean((WindowTransparencyLevel) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(WindowTransparencyLevel))
            {
                return ToWindowTransparencyLevel((bool) value);
            }
            return ToBoolean((WindowTransparencyLevel) value);
        }

        private static bool ToBoolean(WindowTransparencyLevel value)
        {
            return value == WindowTransparencyLevel.None;
        }

        private static WindowTransparencyLevel ToWindowTransparencyLevel(bool value)
        {
            return value ? WindowTransparencyLevel.None : WindowTransparencyLevel.AcrylicBlur;
        }
    }
}