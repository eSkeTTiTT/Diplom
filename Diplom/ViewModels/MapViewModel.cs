using Diplom.Services.Interfaces;
using Diplom.ViewModels.Base;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
		#region Constructors

		public MapViewModel(IMapService mapService)
		{
			_mapService = mapService;
			GetRouteCommandExecute();
		}

		#endregion

		#region Properties

		private readonly IMapService _mapService;

		private string _routeUrl;
		public string RouteUrl
		{
			get => _routeUrl;
			set => SetProperty(ref _routeUrl, value);
		}

		#endregion

		#region Commands

		public ICommand GetRouteCommand => new Command(_ => GetRouteCommandExecute(), _ => true);

		private async void GetRouteCommandExecute()
		{
			var a = await Geolocation.GetLocationAsync();

			var result = _mapService.GetRouteUrl(new()).Result;

			RouteUrl = result;
		}

		#endregion
	}
}
