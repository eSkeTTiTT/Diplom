using Diplom.DAL;
using Diplom.DOMAIN;
using Diplom.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Diplom.ViewModels
{
	public class MapChooseViewModel : BaseViewModel
	{
		#region Lists for choose

		public IReadOnlyCollection<string> KindOfActivities { get; set; }
		public IReadOnlyCollection<string> Persons { get; set; }

		#endregion

		private readonly ApplicationDbContext _context;

		public MapChooseViewModel(ApplicationDbContext context)
		{
			_context = context;

			InitKindOfActivityList();
		}

		private void InitKindOfActivityList() =>
			KindOfActivities = new ReadOnlyCollection<string>(
				_context.KindOfActivities
					.Select(v => v.Name)
					.ToList());
	}
}
