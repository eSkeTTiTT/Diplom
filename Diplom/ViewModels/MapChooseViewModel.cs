using Diplom.DOMAIN;
using Diplom.DOMAIN.DTO;
using Diplom.DOMAIN.Models.Map;
using Diplom.DOMAIN.Test;
using Diplom.Helpers;
using Diplom.ViewModels.Base;
using Newtonsoft.Json;
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

		public IReadOnlyCollection<PersonDto> Recommendations { get; set; }

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

		public INavigation Navigation { get; set; }

		private readonly MapViewModel _mapViewModel;

		private HttpClient _http;

		public MapChooseViewModel(MapViewModel mapViewModel)
		{
			_mapViewModel = mapViewModel;
			_http = new();

			InitCommands();
			InitKindOfActivityList();
			InitRecommendations();
		}

		private async void InitRecommendations()
		{
			var response = await _http.GetAsync(string.Format(DbHelper.URL_GetSuggestions, User.GetId()));
			Recommendations = JsonConvert.DeserializeObject<IReadOnlyCollection<PersonDto>>(await response.ReadAsJsonAsync());
		}

		private void InitKindOfActivityList()
		{
			var response = _http.GetAsync(DbHelper.URL_GetKindOfActivities).Result;
			var jsonResult = response.Content.ReadAsStringAsync().Result;

			KindOfActivities = new ReadOnlyCollection<string>(
				JsonConvert.DeserializeObject<List<KindOfActivity>>(jsonResult)
					.Select(v => v.Name)
					.ToList());
		}

		private void UpdatePersonsList()
		{
			var response = _http.GetAsync(string.Format(DbHelper.URL_GetPersonsFromKind, SelectedKindOfActivity)).Result;
			var jsonResult = response.Content.ReadAsStringAsync().Result;

			Persons = new ReadOnlyCollection<PersonDto>(
				JsonConvert.DeserializeObject<List<PersonDto>>(jsonResult));
		}

		#region Commands

		private void InitCommands()
		{
			ShowMapCommand = new Command(ShowMapCommandExecute, ShowMapCommandCanExecute);
			RecommendCommand = new Command(RecommendCommandExecute, RecommendCommandCanExecute);
		}

		#region Show Map Command

		public ICommand ShowMapCommand { get; private set; }

		private async void ShowMapCommandExecute()
		{
			var response = _http.GetAsync(string.Format(DbHelper.URL_GetLocation, SelectedPerson.LocationId)).Result;
			var jsonResult = response.Content.ReadAsStringAsync().Result;

			var locationDestination = JsonConvert.DeserializeObject<DOMAIN.Location>(jsonResult);

			_mapViewModel.MapSettings = new MapSettings()
			{
				LocationSource = TestMap.TestLocation_1,
				LocationDestination = locationDestination
			};

			await Navigation.PushAsync(new Views.Map(_mapViewModel));
		}

		private bool ShowMapCommandCanExecute() =>
			SelectedKindOfActivity is not null
			&& SelectedPerson is not null;

		#endregion

		#region Recommend Command

		public ICommand RecommendCommand { get; private set; }

		private async void RecommendCommandExecute()
		{
			foreach (var person in Recommendations)
			{
				var response = _http.GetAsync(string.Format(DbHelper.URL_GetLocation, person.LocationId)).Result;
				var jsonResult = response.Content.ReadAsStringAsync().Result;

				var locationDestination = JsonConvert.DeserializeObject<DOMAIN.Location>(jsonResult);

				_mapViewModel.MapSettings = new MapSettings()
				{
					LocationSource = TestMap.TestLocation_1,
					LocationDestination = locationDestination
				};
			}

			await Navigation.PushAsync(new Views.Map(_mapViewModel));
		}

		private bool RecommendCommandCanExecute() => true;

		#endregion

		#endregion
	}
}
