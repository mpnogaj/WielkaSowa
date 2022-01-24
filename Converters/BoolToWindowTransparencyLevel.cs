using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace WielkaSowa.Converters
{
	public class BoolToWindowTransparencyLevel : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (targetType == typeof(WindowTransparencyLevel))
			{
				return ToWindowTransparencyLevel((bool?)value);
			}

			return ToBoolean((WindowTransparencyLevel?)value);
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (targetType == typeof(WindowTransparencyLevel))
			{
				return ToWindowTransparencyLevel((bool?)value);
			}
			return ToBoolean((WindowTransparencyLevel?)value);
		}

		public static bool ToBoolean(WindowTransparencyLevel? value)
		{
			if (value == null) return true;
			return value == WindowTransparencyLevel.None;
		}

		public static WindowTransparencyLevel ToWindowTransparencyLevel(bool? value)
		{
			if (!value.HasValue)
			{
				return WindowTransparencyLevel.None;
			}

			return value.Value
				? WindowTransparencyLevel.None
				: WindowTransparencyLevel.AcrylicBlur;
		}
	}
}