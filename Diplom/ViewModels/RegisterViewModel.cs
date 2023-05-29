using Diplom.DOMAIN;
using Diplom.Helpers;
using Diplom.ViewModels.Base;
using Diplom.Views;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Windows.Input;

namespace Diplom.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{

		#region Propetries

		private HttpClient _http;

		public INavigation Navigation { get; set; }

		private string _password;

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		private string _userName;

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		private string _firstName;

		public string FirstName
		{
			get => _firstName;
			set => SetProperty(ref _firstName, value);
		}

		private string _lastName;

		public string LastName
		{
			get => _lastName;
			set => SetProperty(ref _lastName, value);
		}

		private string _patronymic;

		public string Patronymic
		{
			get => _patronymic;
			set => SetProperty(ref _patronymic, value);
		}

		private DateTime _birthDay;
		public DateTime BirthDay
		{
			get => _birthDay;
			set => SetProperty(ref _birthDay, value);
		}

		private bool _isErrorLabelVisible;
		public bool IsErrorLabelVisible
		{
			get => _isErrorLabelVisible;
			set => SetProperty(ref _isErrorLabelVisible, value);
		}

		#endregion

		#region Commands

		public ICommand RegisterCommand => new Command(async _ => await Register(), _ => true);

		private async Task Register()
		{
			if (!ValidateParams())
				return;

			var content = new StringContent(JsonConvert.SerializeObject(GetUser()), Encoding.UTF8, "application/json");
			await _http.PostAsync(string.Format(DbHelper.URL_PostUser, JsonConvert.SerializeObject(GetUser())), content);

			await Navigation.PushAsync(new Answer(new AnswerViewModel()));
		}

		#endregion

		public RegisterViewModel()
		{
			_http = new HttpClient();
		}

		private bool ValidateParams()
		{
			IsErrorLabelVisible = string.IsNullOrEmpty(UserName)
				|| string.IsNullOrEmpty(FirstName)
				|| string.IsNullOrEmpty(LastName)
				|| string.IsNullOrEmpty(Patronymic)
				|| string.IsNullOrEmpty(Password)
				|| BirthDay == default
				? true
				: false;

			return !IsErrorLabelVisible;
		}

		private User GetUser() =>
			new()
			{
				FirstName = FirstName,
				LastName = LastName,
				Patronymic = Patronymic,
				Password = HashHelper.HashPassword(Password),
				BirthDay = BirthDay,
				UserName = UserName
			};
	}
}
