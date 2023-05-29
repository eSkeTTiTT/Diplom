using Diplom.ViewModels.Base;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.Extensions.FileSystemGlobbing;
using Emgu.CV.Linemod;
using Emgu.CV.Structure;
using System.Windows.Input;
using Diplom.Helpers;
using System.Runtime.InteropServices;
using AVFoundation;
using HomeKit;
using Newtonsoft.Json;
using System.Text;

namespace Diplom.ViewModels
{
	public class CameraViewModel: BaseViewModel
	{
		private string _labelTest = "TEST";
		public string LabelTest
		{
			get => _labelTest;
			set => SetProperty(ref _labelTest, value);
		}

		private string _namePerson;
		public string NamePerson
		{
			get => _namePerson;
			set => SetProperty(ref _namePerson, value);
		}

		private string _imageSource;
		public string ImageSource
		{
			get => _imageSource;
			set => SetProperty(ref _imageSource, value);
		}

		private string _textPerson;
		public string TextPerson
		{
			get => _namePerson;
			set => SetProperty(ref _textPerson, value);
		}

		private bool _isLayoutVisible;
		public bool IsLayoutVisible
		{
			get => _isLayoutVisible;
			set => SetProperty(ref _isLayoutVisible, value);
		}

		private bool _isStopSnapshot;
		public bool IsStopSnaphot
		{
			get => _isStopSnapshot;
			set => SetProperty(ref _isStopSnapshot, value);
		}

		public CameraViewModel()
		{
		}

		#region Commands

		public ICommand KeypointMatchingCommand => new Command(d => KeypointMatching(), _ => true);

		public ICommand CameraViewLoadedCommand => new Command(async v => await CameraViewLoadedCommandExecute(v), _ => true);

		#endregion

		private async void KeypointMatching()
		{
			var http = new HttpClient();
			var response = await http.GetAsync(DbHelper.URL_GetLocation);

			var str = await response.Content.ReadAsStringAsync();

			//LabelTest = str;
		}

		private async Task CameraViewLoadedCommandExecute(object obj)
		{
			var cameraView = obj as CameraView;

			await cameraView.StopCameraAsync();
			await cameraView.StartCameraAsync();

			IsStopSnaphot = true;
			await Task.Run(async () => await MakeSnapShot(cameraView));
		}

		private async Task MakeSnapShot(CameraView cameraView)
		{
			while (IsStopSnaphot)
			{
				Task.Delay(1000).Wait();
				var photo = CvInvoke.Imread(cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG));
				var http = new HttpClient();
				var content = new StringContent(JsonConvert.SerializeObject(photo), Encoding.UTF8, "application/json");
				var response = await http.PostAsync(DbHelper.URL_PostKeyPointMatching, content);

				string id = await response.Content.ReadAsStringAsync();
				if (int.Parse(id) > 0)
				{
					IsStopSnaphot = false;
					IsLayoutVisible = true;
					var photoResponse = await http.GetAsync(string.Format(DbHelper.URL_GetImageById, id));
					var textResponse = await http.GetAsync(string.Format(DbHelper.URL_GetTextById, id));

					TextPerson = await textResponse.Content.ReadAsStringAsync();
					ImageSource = await photoResponse.Content.ReadAsStringAsync();
				}
			}
		}
	}
}
