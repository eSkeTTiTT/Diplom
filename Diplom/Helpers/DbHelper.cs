using CoreFoundation;

namespace Diplom.Helpers;

public static class DbHelper
{
	public const string URL = "http://10.0.2.2:7144/";

	public const string URL_GetKeyPointMatching = "http://10.0.2.2:7144/keypointmatching/get";

	public const string URL_PostKeyPointMatching = "http://10.0.2.2:7144/keypointmatching/post";

	public const string URL_GetLocation = "http://10.0.2.2:7144/db/location?id={0}";

	public const string URL_GetPersonsFromKind = "http://10.0.2.2:7144/db/persons/from-kind?kind={0}";

	public const string URL_GetKindOfActivities = "http://10.0.2.2:7144/db/kind-of-activities";

	public const string URL_PostUser = "http://10.0.2.2:7144/db/add-user?user={0}";

	public const string URL_GetUsers = "http://10.0.2.2:7144/db/users";

	public const string URL_GetUserById = "http://10.0.2.2:7144/db/user-by-id?userId={0}";

	public const string URL_GetPersons = "http://10.0.2.2:7144/db/persons";

	public const string URL_GetPersonById = "http://10.0.2.2:7144/db/person-by-id?personId={0}";

	public const string URL_GetRatings = "http://10.0.2.2:7144/db/ratings";

	public const string URL_GetSuggestions = "http://10.0.2.2:7144/db/suggestions?userId={0}";

	public const string URL_PostRating = "http://10.0.2.2:7144/db/add-rating";

	public const string URL_GetAudioById = "http://10.0.2.2:7144/db/audio-by-id?personId={0}";

	public const string URL_GetVideoById = "http://10.0.2.2:7144/db/video-by-id?personId={0}";

	public const string URL_GetTextById = "http://10.0.2.2:7144/db/text-by-id?personId={0}";

	public const string URL_GetImageById = "http://10.0.2.2:7144/db/image-by-id?personId={0}";
}

