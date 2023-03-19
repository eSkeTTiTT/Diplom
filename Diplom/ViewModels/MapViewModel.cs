using Diplom.ViewModels.Base;
using Esri.ArcGISRuntime.Mapping;

namespace Diplom.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
		#region Constructors

		public MapViewModel()
		{
			SetMap();
		}

		#endregion

		#region Properties

		private Esri.ArcGISRuntime.Mapping.Map _map;
		public Esri.ArcGISRuntime.Mapping.Map Map
		{
			get => _map;
			set => SetProperty(ref _map, value);
		}

		#endregion

		#region Methods

		private void SetMap() =>
			Map = new Esri.ArcGISRuntime.Mapping.Map(BasemapStyle.ArcGISTopographic);

		#endregion
	}
}
