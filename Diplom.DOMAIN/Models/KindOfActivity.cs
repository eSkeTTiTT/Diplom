using System.ComponentModel.DataAnnotations.Schema;

namespace BD_test.Models
{
	[Table("Kind of activities")]
	public class KindOfActivity
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public List<Person> Persons { get; set; } = new();
	}
}
