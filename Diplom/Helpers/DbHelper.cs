namespace Diplom.Helpers;

public static class DbHelper
{
	public const string URL = "http://10.0.2.2:7144/";

	public const string URL_GetKeyPointMatching = "http://10.0.2.2:7144/keypointmatching/get";

	public const string URL_GetLocation = "http://10.0.2.2:7144/db/location?id={0}";

	public const string URL_GetPersonsFromKind = "http://10.0.2.2:7144/db/persons/from-kind?kind={0}";

	public const string URL_GetKindOfActivities = "http://10.0.2.2:7144/db/kind-of-activities";

	public const string URL_PostUser = "http://10.0.2.2:7144/db/add-user?user={0}";

	public const string URL_GetUsers = "http://10.0.2.2:7144/db/users";
}

