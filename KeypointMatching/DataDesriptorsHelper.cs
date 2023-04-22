using Emgu.CV;
using KeypointMatching.Common;
using KeypointMatching.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace KeypointMatching
{
	public static class DataDesriptorsHelper
	{
		public static List<Person>? Persons { get; private set; }

		static DataDesriptorsHelper()
		{
			InitPersons();
		}

		private static void InitPersons()
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var path = Path.Combine(currentDirectory, @"Data\result.txt");
			var fileBytes = File.ReadAllBytes(path);
			var decompressBytes = Brotli.DecompressBytes(fileBytes);
			var stringResult = Encoding.UTF8.GetString(decompressBytes);
			Persons = JsonConvert.DeserializeObject<List<Person>>(stringResult);
		}
	}
}
