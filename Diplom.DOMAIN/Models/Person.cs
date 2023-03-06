using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.DOMAIN
{
	[Table("Persons")]
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Patronymic { get; set; } = null!;


		public int LocationId { get; set; }
		public Location Location { get; set; } = null!;


		public int KindOfActivityId { get; set; }
		public KindOfActivity KindOfActivity { get; set; } = null!;

		public List<Image> Images { get; set; } = new();

		public List<Audio> Audios { get; set; } = new();

		public List<Video> Videos { get; set; } = new();

		public List<Text> Texts { get; set; } = new();

	}
}
