using Diplom.DAL;
using Diplom.Extensions;
using Diplom.Views;
using Esri.ArcGISRuntime.Maui;
using Esri.ArcGISRuntime.Toolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Diplom;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.RegisterAppServices()
			.RegisterViewModels()
			.RegisterViews()
			.UseArcGISRuntime()
			.UseArcGISToolkit();

		// Configuration
		var assembly = Assembly.GetExecutingAssembly();
		using var stream = assembly.GetManifestResourceStream("Diplom.appsettings.json");

		var config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();

		builder.Configuration.AddConfiguration(config);

		return builder.Build();
	}
}
