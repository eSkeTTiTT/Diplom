namespace Diplom.DOMAIN
{
	public class Text
	{
		public int Id { get; set; }

		public int PersonId { get; set; }
		public Person Person { get; set; } = null!;
	}
}
