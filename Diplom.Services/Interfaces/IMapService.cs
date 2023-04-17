using Diplom.DOMAIN.Models.Map;

namespace Diplom.Services.Interfaces;

public interface IMapService
{
	public Task<string> GetRouteUrl(MapSettings mapSettings);
}

