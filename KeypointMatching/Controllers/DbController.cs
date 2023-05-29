using Diplom.DAL;
using Diplom.DOMAIN;
using Diplom.DOMAIN.DTO;
using Diplom.DOMAIN.NewModels;
using KeypointMatching.Recommender.Interfaces;
using KeypointMatching.Recommender.Objects;
using KeypointMatching.Recommender.Systems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KeypointMatching.Controllers
{
	[Controller]
	[Route("db")]
	public class DbController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly DiplomDBContext _diplomDBContext;
		private readonly DiplomDBContext _testContext;
		private readonly UCFSystem _ucfSystem;
		private readonly ICFSystem _icfSystem;

		public DbController(ApplicationDbContext context, DiplomDBContext testContext, IList<IRecommender> recommender)
		{
			_context = context;
			_testContext = testContext;
			_ucfSystem = recommender[0] as UCFSystem;
			_icfSystem = recommender[1] as ICFSystem;
			_diplomDBContext = _testContext;

			var a = _context.Users;
		}

		[HttpGet]
		[Route("persons")]
		public async Task<IActionResult> GetPersons() =>
			new JsonResult(await _diplomDBContext.Persons.ToListAsync());

		[HttpGet]
		[Route("person-by-id")]
		public async Task<IActionResult> GetPersons(int personId) =>
			new JsonResult(await _diplomDBContext.Persons.FirstOrDefaultAsync(v => v.Id == personId));

		[HttpGet]
		[Route("persons/kind-of-activities")]
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
		[Route("persons/location")]
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

		[HttpGet]
		[Route("user-by-id")]
		public async Task<IActionResult> GetUsers(int userId) =>
			new JsonResult(await _context
				.Users
				.FirstOrDefaultAsync(v =>
					v.Id == userId));

		[HttpPost]
		[Route("add-user")]
		public async Task<bool> PostUser(string user)
		{
			var res = JsonConvert.DeserializeObject<User>(user);

			_context.Users.Add(res!);

			await _context.SaveChangesAsync();

			return true;
		}

		[HttpGet]
		[Route("ratings")]
		public IActionResult GetRatings()
		{
			var temp = _diplomDBContext
				.Users
				.Include(v => v.UserActions);

			return new JsonResult(temp.Select(v => new UserActionDTO()
			{
				UserId = v.Id,
				Actions = v.UserActions.ToList()
			}));
		}

		[HttpGet]
		[Route("suggestions")]
		public IActionResult GetSuggestions(int userId)
		{
			var ratings = GetRatings();
			var ratingByUser = GetRatings(userId);

			List<Suggestion> recommendations = null;
			var personsCount = _diplomDBContext.Persons.Count();
			
			_ucfSystem.Init(ratings);
			var recommendations_ucf = _ucfSystem.GetSuggestions(userId, 5);
			_icfSystem.Init(ratings);
			var recommendations_icf = _icfSystem.GetSuggestions(userId, 5);

			return JsonResult(GetSuggestionsFromAlgorithms(
				icf: recommendations_icf, 
				ucf: recommendations_ucf));
		}

		[HttpGet]
		[Route("rating-by-id")]
		public async Task<IActionResult> GetRatings(int userId)
		{
			var temp = await _diplomDBContext
				.Users
				.Include(v => v.UserActions)
				.FirstOrDefaultAsync(v => v.Id == userId);

			return new JsonResult(new UserActionDTO()
			{
				UserId = temp!.Id,
				Actions = temp.UserActions.ToList()
			});
		}

		[HttpPost]
		[Route("add-rating")]
		public async Task<bool> PostRating(UserActionDTO rating)
		{
			foreach (var action in rating.Actions)
				_diplomDBContext.UserActions.Add(action);

			await _diplomDBContext.SaveChangesAsync();

			return true;
		}

		[HttpGet]
		[Route("persons/text-by-id")]
		public async Task<IActionResult> GetText(int personId) =>
			new JsonResult(await _diplomDBContext
				.Texts
				.FirstOrDefaultAsync(v =>
					v.PersonId == personId));

		[HttpGet]
		[Route("persons/video-by-id")]
		public async Task<IActionResult> GetVideo(int personId) =>
			new JsonResult(await _diplomDBContext
				.Videos
				.FirstOrDefaultAsync(v =>
					v.PersonId == personId));

		[HttpGet]
		[Route("persons/audio-by-id")]
		public async Task<IActionResult> GetAudio(int personId) =>
			new JsonResult(await _diplomDBContext
				.Audios
				.FirstOrDefaultAsync(v =>
					v.PersonId == personId));

		[HttpGet]
		[Route("persons/image-by-id")]
		public async Task<IActionResult> GetImage(int personId) =>
			new JsonResult(await _diplomDBContext
				.Images
				.FirstOrDefaultAsync(v =>
					v.PersonId == personId));

		[HttpGet]
		[Route("test-context")]
		public void TestMethod()
		{
			var persons = _testContext.Persons;

			for (int i = 1; i < 6; ++i)
			{
				var random = new Random();
				var bytes = new byte[random.Next(1000, 5000)];
				random.NextBytes(bytes);

				_testContext.Videos.Add(new Videos()
				{
					FileExtension = "mp4",
					FileName = persons.First(p => p.Id == i).Surname + "_video",
					PersonId = i,
					Content = bytes
				});
			}

			_testContext.SaveChanges();
		}
	}
}
