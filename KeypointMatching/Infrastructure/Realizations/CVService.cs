using KeypointMatching.Infrastructure.Interfaces;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace KeypointMatching.Infrastructure.Realizations
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

	public class CVService : ICVService
	{
		public async Task<string> KeypointMatching(object scene)
		{

			var objectImage = CvInvoke.Imread("C:\\visual studio\\vs_projects\\Diplom\\Diplom\\Resources\\Images\\png\\cat.png");
			var sceneImage = CvInvoke.Imread("C:\\visual studio\\vs_projects\\Diplom\\Diplom\\Resources\\Images\\jpg\\scene.jpg");

			// Создание детектора SIFT
			var detector = new Emgu.CV.Features2D.SIFT();

			// Обнаружение ключевых точек и вычисление их дескрипторов на обоих изображениях
			VectorOfKeyPoint keypoints1 = new VectorOfKeyPoint();
			VectorOfKeyPoint keypoints2 = new VectorOfKeyPoint();
			Mat desc1 = new Mat();
			Mat desc2 = new Mat();

			detector.DetectAndCompute(objectImage, null, keypoints1, desc1, false);
			detector.DetectAndCompute(sceneImage, null, keypoints2, desc2, false);

			// Создание объекта Matcher и поиск соответствий между дескрипторами
			var matcher = new BFMatcher(DistanceType.L2Sqr);
			var matches = new VectorOfDMatch();
			matcher.Match(desc1, desc2, matches);
			//matcher.KnnMatch(desc2, matches, 2, null);

			// Отбор наилучших соответствий
			double minDist = double.MaxValue;
			for (int i = 0; i < matches.Size; i++)
			{
				double dist = matches[i].Distance;
				if (dist < minDist) minDist = dist;
			}


			var goodMatches = new List<MDMatch>();
			for (int i = 0; i < matches.Size; i++)
			{
				if (matches[i].Distance <= Math.Max(2 * minDist, 0.02))
				{
					goodMatches.Add(matches[i]);
				}
			}

			var result = goodMatches.Select(v => new MyClass
			{
				Distance = v.Distance,
				TrainIdx = v.TrainIdx,
				ImgIdx = v.ImgIdx,
				QueryIdx = v.QueryIdx
			});

			return JsonSerializer.Serialize(result.First().Distance);
		}
	}
}
