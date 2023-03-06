using System.ComponentModel.DataAnnotations.Schema;

namespace BD_test.Models
{
	[Table("Images")]
	public class Image
	{
		public int Id { get; set; }

		public string ImageUrl { get; set; } = null!;

		public int PersonId { get; set; }
		public Person Person { get; set; } = null!;
	}
}
