using Diplom.DOMAIN.Models.Map;
using Diplom.DOMAIN.Test;
using Diplom.Services.Realizations;
using Diplom.ViewModels;

namespace Diplom.Views;

public partial class MapChoose : ContentPage
{
	private MapViewModel _mapViewModel;

	public MapChoose(MapViewModel mapViewModel, MapChooseViewModel mapChooseViewModel)
	{
		BindingContext = mapChooseViewModel;

		InitializeComponent();

		_mapViewModel = mapViewModel;
	}

	private async void ShowMap_Clicked(object sender, EventArgs e)
	{
		_mapViewModel.MapSettings = new MapSettings()
		{
			LocationSource = TestMap.TestLocation_1,
			LocationDestination = TestMap.TestLocation_2
		};

		await Navigation.PushAsync(new Map(_mapViewModel));
	}
}