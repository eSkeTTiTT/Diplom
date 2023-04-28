using Diplom.DAL;
using Diplom.DOMAIN;
using Diplom.DOMAIN.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KeypointMatching.Controllers
{
	[Controller]
	[Route("db")]
	public class DbController : Controller
	{
		private readonly ApplicationDbContext _context;

		public DbController(ApplicationDbContext context)
		{
			_context = context;

			var a = _context.Users;
		}

		[HttpGet]
		[Route("kind-of-activities")]
		public async Task<IActionResult> GetKindOfActivities() =>
			new JsonResult(await _context.KindOfActivities.ToListAsync());

		[HttpGet]
		[Route("persons/from-kind")]
		public async Task<IActionResult> GetPersonsFromKind(string kind)
		{
			return new JsonResult(await _context.Persons
					.Where(v => v.KindOfActivity.Name == kind)
					.Select(v => new PersonDto
					{
						LocationId = v.LocationId,
						Name = v.Name,
						Surname = v.Surname,
						Patronymic = v.Patronymic,
						BornDate = v.BornDate,
						DeathDate = v.DeathDate
					})
					.ToListAsync());
		}

		[HttpGet]
		[Route("location")]
		public async Task<IActionResult> GetLocation(int id)
		{
			return new JsonResult(await _context.Locations
				.FirstOrDefaultAsync(v =>
					v.Id == id));
		}

		[HttpGet]
		[Route("users")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users
				.Select(u => new UserDTO
				{
					UserName = u.UserName,
					Password = u.Password
				})
				.ToListAsync();

			return new JsonResult(users);
		}

		[HttpPost]
		[Route("add-user")]
		public async Task<bool> PostUser(string user)
		{
			var res = JsonConvert.DeserializeObject<User>(user);

			_context.Users.Add(res!);

			await _context.SaveChangesAsync();

			return true;
		}
	}
}
