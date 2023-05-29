using Diplom.DOMAIN.DTO;
using Diplom.DOMAIN.NewModels;
using KeypointMatching.Recommender.Interfaces;
using KeypointMatching.Recommender.Objects;

namespace KeypointMatching.Recommender.Systems;

public class ICFSystem : IRecommender
{
	public UserActionDTO Ratings { get; set; }
	public void Init(UserActionDTO actions)
	{
		Ratings = actions;
	}

	private List<int> GetHighestRatedPersonsForUser(int userIndex)
	{
		List<Tuple<int, double>> items = new List<Tuple<int, double>>();
		for (int personIndex = 0; personIndex < Ratings.PersonIndexToID.Count; personIndex++)
		{
			if (Ratings.Users[userIndex].PersonRatings[personIndex] != 0)
			{
				items.Add(new Tuple<int, double>(personIndex,
					Ratings.Users[userIndex].PersonRatings[personIndex]));
			}
		}
		items.Sort((c, n) => n.Item2.CompareTo(c.Item2));
		return items.Select(x => x.Item1).ToList();
	}

	public double GetRating(int userId, int PersonId)
	{
		int userIndex = Ratings.UserIndexToID.IndexOf(userId);
		int PersonIndex = Ratings.PersonIndexToID.IndexOf(PersonId);

		var userRatings = Ratings.Users[userIndex].PersonRatings.Where(x => x != 0);
		var PersonRatings = Ratings.Users.Select(x => x.PersonRatings[PersonIndex]).Where(x => x != 0);

		double averageUser = userRatings.Count() > 0 ? userRatings.Average() : 0;
		double averagePerson = PersonRatings.Count() > 0 ? PersonRatings.Average() : 0;

		// For now, just return the average rating across this user and Person
		return averagePerson > 0 && averageUser > 0 ? (averageUser + averagePerson) / 2.0 : Math.Max(averageUser, averagePerson);
	}

	public List<Suggestion> GetSuggestions(int userId, int numSuggestions)
	{
		int userIndex = Ratings.UserIndexToID.IndexOf(userId);
		List<int> Persons = GetHighestRatedPersonsForUser(userIndex).Take(5).ToList();
		List<Suggestion> suggestions = new List<Suggestion>();

		foreach (int PersonIndex in Persons)
		{
			int PersonId = Ratings.PersonIndexToID[PersonIndex];
			List<PersonRating> neighboringPersons = GetNearestNeighbors(PersonId, neighborCount);

			foreach (PersonRating neighbor in neighboringPersons)
			{
				int neighborPersonIndex = Ratings.PersonIndexToID.IndexOf(neighbor.PersonID);

				double averagePersonRating = 0.0;
				int count = 0;
				for (int userRatingIndex = 0; userRatingIndex < Ratings.UserIndexToID.Count; userRatingIndex++)
				{
					if (transposedRatings[neighborPersonIndex][userRatingIndex] != 0)
					{
						averagePersonRating += transposedRatings[neighborPersonIndex][userRatingIndex];
						count++;
					}
				}
				if (count > 0)
				{
					averagePersonRating /= count;
				}

				suggestions.Add(new Suggestion(userId, neighbor.PersonID, averagePersonRating));
			}
		}

		suggestions.Sort((c, n) => n.Rating.CompareTo(c.Rating));

		return suggestions.Take(numSuggestions).ToList();
	}

	private List<PersonRating> GetNearestNeighbors(int PersonId, int numPersons)
	{
		List<PersonRating> neighbors = new List<PersonRating>();
		int mainPersonIndex = Ratings.PersonIndexToID.IndexOf(PersonId);

		for (int PersonIndex = 0; PersonIndex < Ratings.PersonIndexToID.Count; PersonIndex++)
		{
			int searchPersonId = Ratings.PersonIndexToID[PersonIndex];

			double score = comparer.CompareVectors(transposedRatings[mainPersonIndex], transposedRatings[PersonIndex]);

			neighbors.Add(new PersonRating(searchPersonId, score));
		}

		neighbors.Sort((c, n) => n.Rating.CompareTo(c.Rating));

		return neighbors.Take(numPersons).ToList();
	}

	public double GetRating(int userId, int PersonId)
	{
		int userIndex = Ratings.UserIndexToID.IndexOf(userId);
		int PersonIndex = Ratings.PersonIndexToID.IndexOf(PersonId);

		var userRatings = Ratings.Users[userIndex].PersonRatings.Where(x => x != 0);
		var PersonRatings = Ratings.Users.Select(x => x.PersonRatings[PersonIndex]).Where(x => x != 0);

		double averageUser = userRatings.Count() > 0 ? userRatings.Average() : 0;
		double averagePerson = PersonRatings.Count() > 0 ? PersonRatings.Average() : 0;

		// For now, just return the average rating across this user and Person
		return averagePerson > 0 && averageUser > 0 ? (averageUser + averagePerson) / 2.0 : Math.Max(averageUser, averagePerson);
	}

	private List<PersonRating> GetNearestNeighbors(int personId, int numPersons)
	{
		List<PersonRating> neighbors = new List<PersonRating>();
		int mainPersonIndex = Ratings.PersonIndexToID.IndexOf(personId);

		for (int personIndex = 0; personIndex < Ratings.PersonIndexToID.Count; personIndex++)
		{
			int searchPersonId = Ratings.PersonIndexToID[personIndex];

			double score = comparer.CompareVectors(transposedRatings[mainPersonIndex], transposedRatings[personIndex]);

			neighbors.Add(new PersonRating(searchPersonId, score));
		}

		neighbors.Sort((c, n) => n.Rating.CompareTo(c.Rating));

		return neighbors.Take(numPersons).ToList();
	}
}
