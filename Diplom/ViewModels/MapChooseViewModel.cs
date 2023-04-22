using Diplom.DAL;
using Diplom.ViewModels.Base;
using Diplom.ViewModels.Contracts;
using System.Collections.ObjectModel;

namespace Diplom.ViewModels
{
	public class MapChooseViewModel : BaseViewModel
	{
		#region Lists for choose

		public IReadOnlyCollection<string> KindOfActivities { get; set; }

		private IReadOnlyCollection<PersonDto> _persons;
		public IReadOnlyCollection<PersonDto> Persons
		{
			get => _persons;
			set => SetProperty(ref _persons, value);
		}

		#endregion

		#region INotifyPropertyChanged Properties

		private string _selectedKindOfActivity;
		public string SelectedKindOfActivity
		{
			get => _selectedKindOfActivity;
			set
			{
				if (value != _selectedKindOfActivity)
				{
					IsPersonsCollectionVisible = true;
					_selectedKindOfActivity = value;

					UpdatePersonsList();
				}
			}
		}

		private bool _isPersonsCollectionVisible;
		public bool IsPersonsCollectionVisible
		{
			get => _isPersonsCollectionVisible;
			set => SetProperty(ref _isPersonsCollectionVisible, value);
		}

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

		private void UpdatePersonsList() =>
			Persons = new ReadOnlyCollection<PersonDto>(
				_context.Persons
					.Where(v => v.KindOfActivity.Name == SelectedKindOfActivity)
					.Select(v => new PersonDto
					{
						Name = v.Name,
						Surname = v.Surname,
						Patronymic = v.Patronymic,
						BornDate = v.BornDate,
						DeathDate = v.DeathDate
					})
					.ToList());
	}
}
