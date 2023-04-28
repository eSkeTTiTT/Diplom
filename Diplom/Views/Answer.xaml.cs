using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Answer : ContentPage
{
	public Answer(AnswerViewModel VM)
	{
		VM.Navigation = Navigation;
		BindingContext = VM;

		InitializeComponent();
	}
}