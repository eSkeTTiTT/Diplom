using Diplom.DOMAIN.DTO;
using KeypointMatching.Recommender.Interfaces;
using KeypointMatching.Recommender.Objects;

namespace KeypointMatching.Recommender.Systems;

public class UCFSystem : IRecommender
{
	public UserActionDTO Ratings { get; set; }

	public void Init(UserActionDTO actions)
	{
		Ratings = actions;
	}

	private List<UserPersonRatings> GetNearestNeighbors(UserPersonRatings user, int numUsers)
	{
		List<UserPersonRatings> neighbors = new List<UserPersonRatings>();
		for (int i = 0; i < Ratings.Users.Count; i++)
		{
			if (Ratings.Users[i].UserID == user.UserID)
			{
				Ratings.Users[i].Score = double.NegativeInfinity;
			}
			else
			{
				Ratings.Users[i].Score =
					comparer.CompareVectors(Ratings.Users[i].PersonRatings, user.PersonRatings);
			}
		}
		var similarUsers = Ratings.Users.OrderByDescending(x => x.Score);
		return similarUsers.Take(numUsers).ToList();
	}

	public double GetRating(int userId, int personId)
	{
		UserPersonRatings user = Ratings.Users.FirstOrDefault(x => x.UserID == userId);
		List<UserPersonRatings> neighbors = GetNearestNeighbors(user, neighborCount);

		return GetRating(user, neighbors, personId);
	}

	private double GetRating(UserPersonRatings user, List<UserPersonRatings> neighbors, int personId)
	{
		int personIndex = Ratings.PersonIndexToID.IndexOf(personId);
		var nonZero = user.PersonRatings.Where(x => x != 0);
		double avgUserRating = nonZero.Count() > 0 ? nonZero.Average() : 0.0;
		double score = 0.0;
		int count = 0;
		for (int u = 0; u < neighbors.Count; u++)
		{
			var nonZeroRatings = neighbors[u].PersonRatings.Where(x => x != 0);
			double avgRating = nonZeroRatings.Count() > 0 ? nonZeroRatings.Average() : 0.0;

			if (neighbors[u].PersonRatings[personIndex] != 0)
			{
				score += neighbors[u].PersonRatings[personIndex] - avgRating;
				count++;
			}
		}
		if (count > 0)
		{
			score /= count;
			score += avgUserRating;
		}
		return score;
	}

	public List<Suggestion> GetSuggestions(int userId, int numSuggestions)
	{
		int userIndex = Ratings.UserIndexToID.IndexOf(userId);
		UserPersonRatings user = Ratings.Users[userIndex];
		List<Suggestion> suggestions = new List<Suggestion>();

		var neighbors = GetNearestNeighbors(user, neighborCount);

		for (int personIndex = 0; personIndex < Ratings.PersonIndexToID.Count; personIndex++)
		{
			if (user.PersonRatings[personIndex] == 0)
			{
				double score = 0.0;
				int count = 0;
				for (int u = 0; u < neighbors.Count; u++)
				{
					if (neighbors[u].PersonRatings[personIndex] != 0)
					{  
						score += neighbors[u].PersonRatings[personIndex] - ((u + 1.0) / 100.0);
						count++;
					}
				}
				if (count > 0)
				{
					score /= count;
				}

				suggestions.Add(new Suggestion(userId, Ratings.PersonIndexToID[personIndex], score));
			}	
		}

		suggestions.Sort((c, n) => n.Rating.CompareTo(c.Rating));

		return suggestions.Take(numSuggestions).ToList();
	}
}