using Diplom.DAL;
using Diplom.DOMAIN;
using Diplom.DOMAIN.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

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
	}
}
