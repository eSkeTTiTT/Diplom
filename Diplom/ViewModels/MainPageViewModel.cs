using Diplom.ViewModels.Base;
using Diplom.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{

		#region Properties

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

		#endregion

		#region Commands

		private ICommand _regiterCommand;

		public ICommand RegisterCommand { 
			get => _regiterCommand ??= new Command(_ => RegisterExecute(), _ => RegisterCanExecute());
			set => _regiterCommand = value;
		}

		private async void RegisterExecute()
		{
			await Navigation.PushAsync(new Register(new RegisterViewModel()));
		}

		private static bool RegisterCanExecute() => true;

		private ICommand _signInCommand;

		public ICommand SignInCommand => _regiterCommand ??= new Command(_ => SignInExecute(), _ => SignInCanExecute());

		private void SignInExecute()
		{
			//Navigation.PushAsync();
		}

		private static bool SignInCanExecute() => true;

		#endregion


		public MainPageViewModel()
		{

		}
	}
}
