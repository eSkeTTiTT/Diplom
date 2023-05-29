using Diplom.DOMAIN.DTO;
using Diplom.DOMAIN.NewModels;
using KeypointMatching.Recommender.Objects;

namespace KeypointMatching.Recommender.Interfaces;

public interface IRecommender
{
	public UserActionDTO Ratings { get; set; }
	public void Init(UserActionDTO actions);
	public List<Suggestion> GetSuggestions(int userId, int numSuggestions);
	public double GetRating(int userId, int personId);
}
