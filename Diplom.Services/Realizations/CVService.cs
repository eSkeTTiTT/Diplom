using Diplom.Services.Interfaces;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Diplom.Services.Realizations
{
	public class CVService : ICVService
	{
		public async Task KeypointMatching(object scene)
		{

			var objectImage = CvInvoke.Imread("C:\\Users\\Artem Aleksandrovich\\source\\repos\\Diplom\\Diplom\\Resources\\Images\\png\\cat.png");
			var sceneImage = CvInvoke.Imread("C:\\Users\\Artem Aleksandrovich\\source\\repos\\Diplom\\Diplom\\Resources\\Images\\jpg\\scene.jpg");

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

		}
	}
}
