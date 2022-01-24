using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WielkaSowa.Services;

namespace WielkaSowa.Models
{
	public class Multipliers
	{
		public const string DefaultWildcard = "$DOMYŚLNA$";
		
		#region Points multipliers
		// ReSharper disable InconsistentNaming
		// Zachowanie
		public int MWzor { get; init; } = 5;
		public int MBdb { get; init; } = 3;
		public int MDb { get; init; } = 1;
		public int MPop { get; init; } = 0;
		public int MNOdp { get; init; }= -2;
		public int MNag { get; init; }= -15;

		// Olimpiady
		public int MOSz { get; init; } = 10;
		public int MOOk { get; init; } = 25;
		public int MOCt { get; init; } = 75;
		public int MOMn { get; init; } = 150;

		// Konkursy
		public int MKSz { get; init; } = 5;
		public int MKRj { get; init; } = 10;
		public int MKCt { get; init; } = 50;
		public int MKMn { get; init; } = 100;

		// Wycieczki Klasowe 1-d
		public int MWK1 { get; init; } = 20;
		// 2 dniowe i dluzsze
		public int MWK2 { get; init; } = 50;
		// Przedsiewziecia klasowe
		public int MPK { get; init; } = 5;

		// Programy artystyczne
		public int MSzPA { get; init; } = 10;
		// Kiermasze
		public int MSzKier { get; init; } = 50;
		// Organizacja imprez
		public int MSzOImp { get; init; } = 50;
		// Pomoc w organnizacji imprez
		public int MSzPImp { get; init; } = 25;

		// Parlament
		public int MPU { get; init; } = 40;
		// Poczet sztandarowy
		public int MPSz { get; init; } = 20;

		// Wolontariaty, harcerstwo itp.
		public int MPSzWol { get; init; } = 40;
		public int MPSzMRM { get; init; } = 40;
		public int MPSzHar { get; init; } = 40;
		public int MPSzPTH { get; init; } = 40;
		public int MPSzZbior { get; init; } = 25;
		// ReSharper restore InconsistentNaming
		#endregion

		public static Multipliers Default => new();
		
		private Multipliers() { }

		/// <summary>
		/// Loads multipliers from json file
		/// </summary>
		/// <param name="pathToFile">Path to json file</param>
		public static async Task<Multipliers> CreateMultipliers(string pathToFile)
		{
			try
			{
				string json = await new FileManager(pathToFile, false).Read();
				var fromFile = JsonConvert.DeserializeObject<Multipliers>(json);
				return fromFile ?? Default;
			}
			catch (JsonException ex)
			{
				Console.WriteLine(ex);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return Default;
		}
	}
}