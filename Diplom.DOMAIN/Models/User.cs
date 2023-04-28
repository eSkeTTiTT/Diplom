namespace Diplom.DOMAIN;

public class User
{
	public int Id { get; set; }

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public string Patronymic { get; set; } = null!;

	public string UserName { get; set; } = null!;

	public DateTime BirthDay { get; set; }

	public string Password { get; set; } = null!;
}

