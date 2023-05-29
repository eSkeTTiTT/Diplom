using Diplom.ViewModels;

namespace Diplom.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel VM)
	{
		VM.Navigation = Navigation;
		BindingContext = VM;


		InitializeComponent();
	}
}

