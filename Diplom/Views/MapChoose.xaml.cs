using Diplom.ViewModels;

namespace Diplom.Views;

public partial class MapChoose : ContentPage
{
	public MapChoose(MapChooseViewModel mapChooseViewModel)
	{
		mapChooseViewModel.Navigation = Navigation;
		BindingContext = mapChooseViewModel;

		InitializeComponent();
	}
}