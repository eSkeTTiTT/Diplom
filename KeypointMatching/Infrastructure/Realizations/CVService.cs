using KeypointMatching.Infrastructure.Interfaces;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using KeypointMatching.Contracts;

namespace KeypointMatching.Infrastructure.Realizations
{

	public class CVService : ICVService
	{
		public async Task<int> KeypointMatching(Mat photo)
		{

			//var sceneImage = CvInvoke.Imread("C:\\visual studio\\vs_projects\\Diplom\\Diplom\\Resources\\Images\\jpg\\scene.jpg");

			// Создание детектора SIFT
			var detector = new Emgu.CV.Features2D.SIFT();
			List<Person>? persons = DataDesriptorsHelper.Persons;
			List<ResultsOfMatching> resultsOfmatching = new List<ResultsOfMatching>();

			// Обнаружение ключевых точек
			VectorOfKeyPoint keypoints1 = new VectorOfKeyPoint();
			Mat desc1 = new Mat();
			detector.DetectAndCompute(photo, null, keypoints1, desc1, false);

			var matcher = new BFMatcher(DistanceType.L2Sqr);
			var matches = new VectorOfDMatch();
			int goodMatches;

			
			foreach (var person in persons)
			{
				goodMatches = 0;
				foreach (var currentDesc in person.Descriptors)
				{
					matcher.Match(desc1, currentDesc, matches);


					// Отбор наилучших соответствий
					double minDist = double.MaxValue;
					for (int i = 0; i < matches.Size; i++)
					{
						double dist = matches[i].Distance;
						if (dist < minDist) minDist = dist;
					}


					for (int i = 0; i < matches.Size; i++)
					{
						if (matches[i].Distance <= Math.Max(2 * minDist, 0.02))
						{
							++goodMatches;
						}
					}
				}
				resultsOfmatching.Add(new() { GoodMatches = goodMatches, Id = person.ID });
	
			}

			int maxGoodMatches = resultsOfmatching.Max(x => x.GoodMatches);
			if (maxGoodMatches < 100)
				return 0;

			return resultsOfmatching.FirstOrDefault(x => x.GoodMatches == maxGoodMatches).Id;
		}
	}
}
