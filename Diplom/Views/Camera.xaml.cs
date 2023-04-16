using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Camera : ContentPage
{
	public Camera(CameraViewModel VM)
	{
		BindingContext = VM;

		InitializeComponent();
	}
}