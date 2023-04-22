namespace Diplom.ViewModels.Contracts
{
    public class PersonDto
    {
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Patronymic { get; set; } = null!;
		public DateTime? BornDate { get; set; }
		public DateTime? DeathDate { get; set; }
	}
}
