using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Register : ContentPage
{
	public Register(RegisterViewModel VM)
	{
		VM.Navigation = Navigation;
		BindingContext = VM;

		InitializeComponent();
	}
}