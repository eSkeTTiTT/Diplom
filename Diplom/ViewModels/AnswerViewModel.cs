using Diplom.ViewModels.Base;
using System.Windows.Input;

namespace Diplom.ViewModels;

public class AnswerViewModel : BaseViewModel
{
	public INavigation Navigation { get; set; }

	public AnswerViewModel()
	{
	}

	#region Commands

	public ICommand AnswerCommand => new Command(_ => AnswerExecute(), _ => true);

	private async void AnswerExecute()
	{
		if (!ValidateParams())
			return;

		await Navigation.PopToRootAsync();
	}

	private bool ValidateParams()
	{
		return true;
	}

	#endregion
}

