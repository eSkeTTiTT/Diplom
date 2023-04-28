using Diplom.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{

		#region Propetries

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

		#endregion
		public RegisterViewModel()
		{

		}

	}
}
