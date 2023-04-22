using Diplom.DOMAIN.Models.Map;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Realizations;

public class MapService : IMapService
{
	#region Templates

	private const string ROUTE_TEMPLATE = @"https://yandex.ru/maps/?rtext={0},{1}~{2},{3}&rtt=pd";

	#endregion

	public Task<string> GetRouteUrl(MapSettings mapSettings) =>
		Task.FromResult(
			string.Format(ROUTE_TEMPLATE,
				mapSettings.LocationSource.Latitude,
				mapSettings.LocationSource.Longitude,
				mapSettings.LocationDestination.Latitude,
				mapSettings.LocationDestination.Longitude));
}

