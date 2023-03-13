using Diplom.DAL;
using Diplom.Views;

namespace Diplom.Extensions
{
    public static class DIRegistrator
    {
        #region VM

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiBuider) =>
            mauiBuider;

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
            mauiBuider.Services.AddTransient<MainPage>();

            return mauiBuider;
        }

        #endregion
    }
}
