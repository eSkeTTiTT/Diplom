using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.DOMAIN
{
	[Table("Locations")]
	public class Location
	{
		public int Id { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public Person Person { get; set; } = null!;
	}
}
