using Diplom.DAL;
using Diplom.DOMAIN.Models.Map;
using Diplom.DOMAIN.Test;
using Diplom.ViewModels.Base;
using Diplom.ViewModels.Contracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
					SetProperty(ref _selectedKindOfActivity, value);

					UpdatePersonsList();
					SelectedPerson = null;
				}
			}
		}

		private PersonDto _selectedPerson;
		public PersonDto SelectedPerson
		{
			get => _selectedPerson;
			set
			{
				SetProperty(ref _selectedPerson, value);
				((Command)ShowMapCommand).ChangeCanExecute();
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

		public INavigation Navigation { get; set; }

		private readonly MapViewModel _mapViewModel;

		public MapChooseViewModel(ApplicationDbContext context, MapViewModel mapViewModel)
		{
			_context = context;
			_mapViewModel = mapViewModel;

			InitCommands();
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

		#region Commands

		private void InitCommands()
		{
			ShowMapCommand = new Command(_ => ShowMapCommandExecute(), _ => ShowMapCommandCanExecute());
		}

		public ICommand ShowMapCommand { get; private set; }

		private async void ShowMapCommandExecute()
		{
			_mapViewModel.MapSettings = new MapSettings()
			{
				LocationSource = TestMap.TestLocation_1,
				LocationDestination = TestMap.TestLocation_2
			};

			await Navigation.PushAsync(new Views.Map(_mapViewModel));
		}

		private bool ShowMapCommandCanExecute() =>
			SelectedKindOfActivity is not null
			&& SelectedPerson is not null;

		#endregion
	}
}
