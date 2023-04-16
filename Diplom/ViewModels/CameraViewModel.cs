using Diplom.ViewModels.Base;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.Extensions.FileSystemGlobbing;
using Emgu.CV.Linemod;
using Emgu.CV.Structure;
using Diplom.Services.Interfaces;

namespace Diplom.ViewModels
{
	public class CameraViewModel: BaseViewModel
	{
		#region Properties

		private readonly ICVService _CVService;

		#endregion
		public CameraViewModel(ICVService CVService)
		{
			_CVService = CVService;
		}
	}
}
