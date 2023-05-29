using Diplom.Services.Interfaces;
using Diplom.Services.Realizations;
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
            mauiBuider.Services.AddScoped<CameraViewModel>();
            mauiBuider.Services.AddScoped<MapChooseViewModel>();
            mauiBuider.Services.AddScoped<MainPageViewModel>();
            mauiBuider.Services.AddScoped<RegisterViewModel>();
            mauiBuider.Services.AddScoped<AnswerViewModel>();
			mauiBuider.Services.AddScoped<ProfileViewModel>();
            mauiBuider.Services.AddScoped<ArticleViewModel>();

			return mauiBuider;
        }

        #endregion

        #region Services

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiBuider)
        {
            //mauiBuider.Services.AddScoped<ApplicationDbContext>();
            mauiBuider.Services.AddScoped<IMapService, MapService>();

            return mauiBuider;
        }

        #endregion

        #region Views

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiBuider)
        {
            mauiBuider.Services.AddSingleton<MainPage>();
			mauiBuider.Services.AddSingleton<Camera>();
			mauiBuider.Services.AddSingleton<Views.Map>();
			mauiBuider.Services.AddSingleton<Views.MapChoose>();
            mauiBuider.Services.AddSingleton<Views.Register>();
            mauiBuider.Services.AddSingleton<Views.Answer>();
			mauiBuider.Services.AddSingleton<Views.Profile>();
            mauiBuider.Services.AddSingleton<Views.Article>();

			return mauiBuider;
        }

        #endregion
    }
}
