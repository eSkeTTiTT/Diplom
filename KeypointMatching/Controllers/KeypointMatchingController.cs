using KeypointMatching.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KeypointMatching.Controllers
{
	[Controller]
	public class KeypointMatchingController : Controller
	{
		#region Properties

		private readonly ICVService _cvService;

		#endregion

		public KeypointMatchingController(ICVService cvService)
		{
			_cvService = cvService;
		}

		[HttpGet]
		public async Task Get()
		{
			await HttpContext.Response.WriteAsync(await _cvService.KeypointMatching(0));
		}

		[HttpGet]
		public async Task Index()
		{
			await HttpContext.Response.WriteAsync("loosodoasd");
		}
	}
}
