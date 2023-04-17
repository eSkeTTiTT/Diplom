using Diplom.ViewModels.Base;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.Extensions.FileSystemGlobbing;
using Emgu.CV.Linemod;
using Emgu.CV.Structure;
using Diplom.Infrastructure.Interfaces;
using System.Windows.Input;
using System.Net.Http.Json;

namespace Diplom.ViewModels
{
	[Serializable]
	public class MyClass
	{
		//
		// Summary:
		//     Query descriptor index
		public int QueryIdx;

		//
		// Summary:
		//     Train descriptor index
		public int TrainIdx;

		//
		// Summary:
		//     Train image index
		public int ImgIdx;

		//
		// Summary:
		//     Distance
		public float Distance;
	}

	public class CameraViewModel: BaseViewModel
	{
		private string _labelTest = "SUCK MY DICK";
		public string LabelTest
		{
			get => _labelTest;
			set => SetProperty(ref _labelTest, value);
		}

		public CameraViewModel()
		{
		}

		#region Commands

		public ICommand KeypointMatchingCommand => new Command(d => KeypointMatching(), _ => true);

		#endregion

		private async void KeypointMatching()
		{
			var http = new HttpClient();
			var response = await http.GetAsync("http://10.0.2.2:7144/keypointmatching/get");

			var str = await response.Content.ReadAsStringAsync();

			//LabelTest = str;
		}
	}
}
