using Diplom.DOMAIN;
using Diplom.DOMAIN.DTO;
using Diplom.Helpers;
using Diplom.ViewModels.Base;
using Diplom.Views;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Windows.Input;

namespace Diplom.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{

		#region Properties

		private HttpClient _http;

		public INavigation Navigation { get; set; }

		private string _userName;

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		private string _password;

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		private bool _isErrorVisible;
		public bool IsErrorVisible
		{
			get => _isErrorVisible;
			set => SetProperty(ref _isErrorVisible, value);
		}

		#endregion

		#region Commands

		public ICommand RegisterCommand => new Command(_ => RegisterExecute(), _ => RegisterCanExecute());

		private async void RegisterExecute()
		{
			await Navigation.PushAsync(new Register(new RegisterViewModel()));
		}

		private static bool RegisterCanExecute() => true;

		public ICommand SignInCommand => new Command(_ => SignInExecute(), _ => SignInCanExecute());

		private async void SignInExecute()
		{
			var response = await _http.GetAsync(DbHelper.URL_GetUsers);

			var str = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<UserDTO>>(str);

			bool isAccess = users.FirstOrDefault(u =>
				u.Password == HashHelper.HashPassword(Password)
				&& u.UserName == UserName) is not null;

			IsErrorVisible = !isAccess;

			if (isAccess)
				await Navigation.PushAsync(new Profile(new ProfileViewModel()));
		}

		private static bool SignInCanExecute() => true;

		#endregion

		public MainPageViewModel()
		{
			_http = new HttpClient();
		}
	}
}
