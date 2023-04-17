using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Map : ContentPage
{
	public Map(MapViewModel mapVM)
	{
		BindingContext = mapVM;

		InitializeComponent();
	}
}