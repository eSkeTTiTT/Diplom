using Diplom.DOMAIN.NewModels;

namespace Diplom.DOMAIN.DTO;

#nullable disable

public class UserActionDTO
{
	public int UserId { get; set; }

	public List<UserActions> Actions { get; set; }
}
