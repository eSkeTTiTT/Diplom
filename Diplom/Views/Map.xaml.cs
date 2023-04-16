using Diplom.ViewModels;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;

namespace Diplom.Views;

public partial class Map : ContentPage
{
	public Map(MapViewModel mapVM)
	{
		BindingContext = mapVM;

		InitializeComponent();

		MapPoint mapCenterPoint = new MapPoint(-118.805, 34.027, SpatialReferences.Wgs84);
		MainMapView.SetViewpoint(new Viewpoint(mapCenterPoint, 100000));
	}
}