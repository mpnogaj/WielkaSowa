﻿using Avalonia;

namespace WielkaSowa
{
	class Program
	{
		public static void Main(string[] args)
			=> BuildAvaloniaApp()
				.StartWithClassicDesktopLifetime(args);

		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace();
	}
}