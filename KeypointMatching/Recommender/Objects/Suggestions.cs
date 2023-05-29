namespace KeypointMatching.Recommender.Objects;

public class Suggestion
{
	public int UserId { get; set; }

	public int PersonId { get; set; }

	public double Rating { get; set; }

	public Suggestion(int userId, int personId, double assurance)
	{
		UserId = userId;
		PersonId = personId;
		Rating = assurance;
	}
}
