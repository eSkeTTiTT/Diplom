using Diplom.ViewModels;

namespace Diplom.Views;

public partial class Map : ContentPage
{
	public Map(MapViewModel mapVM)
	{
		BindingContext = mapVM;

		InitializeComponent();
	}

	private async void Map_Back_To_Choose(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}