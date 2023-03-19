namespace Diplom;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override void OnStart()
	{
		base.OnStart();

		Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPK8868462a92a3428d8aae7b76d8afbc29JvtGzMd1YvpVkvkoSZL7S01NamK-sfAqGeBjwz2f_LAlYjTCBaN3qJ5iS5RvQGrN";
	}
}
