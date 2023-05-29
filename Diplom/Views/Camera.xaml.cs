using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Camera : ContentPage
{
	public Camera(CameraViewModel VM)
	{
		BindingContext = VM;

		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Article(new ArticleViewModel()));
    }
}