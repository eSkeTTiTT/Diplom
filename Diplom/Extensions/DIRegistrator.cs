using Diplom.DAL;
using Diplom.ViewModels;
using Diplom.Views;

namespace Diplom.Extensions
{
    public static class DIRegistrator
    {
        #region VM

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiBuider)
        {
            mauiBuider.Services.AddScoped<MapViewModel>();

            return mauiBuider;
        }

        #endregion

        #region Services

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiBuider)
        {
            mauiBuider.Services.AddScoped<ApplicationDbContext>();

            return mauiBuider;
        }

        #endregion

        #region Views

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiBuider)
        {
            mauiBuider.Services.AddSingleton<MainPage>();
			mauiBuider.Services.AddSingleton<Camera>();
			mauiBuider.Services.AddSingleton<Views.Map>();

			return mauiBuider;
        }

        #endregion
    }
}
