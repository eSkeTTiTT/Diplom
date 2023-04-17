using Diplom.DOMAIN.Models.Map;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Realizations;

public class MapService : IMapService
{
	#region Templates

	private const string ROUTE_TEMPLATE = @"https://yandex.ru/maps/213/moscow/?ll=37.554845%2C55.724824&mode=routes&rtext=55.724800%2C37.553200~55.725057%2C37.553663&rtt=pd&ruri=ymapsbm1%3A%2F%2Forg%3Foid%3D13786652536~ymapsbm1%3A%2F%2Forg%3Foid%3D197265533383&z=19.15";

	#endregion

	public Task<string> GetRouteUrl(MapSettings mapSettings)
	{
		return Task.FromResult(ROUTE_TEMPLATE);
	}
}

