using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel VM)
	{
		VM.Navigation = Navigation;
		BindingContext = VM;

		InitializeComponent();
	}
}