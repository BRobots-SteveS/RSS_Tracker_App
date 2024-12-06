using CommunityToolkit.Maui;
using InputKit.Shared.Controls;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Mobile_App.ViewModels;
using UraniumUI;
using UraniumUI.Icons.MaterialSymbols;

namespace Rss_Mobile_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddMaterialSymbolsFonts();

                });

            builder.Services.AddCommunityToolkitDialogs();
            builder.Services
                .AddSingleton<INavigationService, MauiNavigationService>()
                .AddSingleton<AuthorRepository>()
                .AddSingleton<EpisodeRepository>()
                .AddSingleton<FeedRepository>()
                .AddSingleton<UserRepository>()
                .AddSingleton<AccountViewModel>()
                .AddSingleton<AuthorListViewModel>()
                .AddSingleton<EpisodeDetailViewModel>()
                .AddSingleton<FeedDetailViewModel>()
                .AddSingleton<FeedListViewModel>()
                .AddSingleton<LoginViewModel>();
            return builder.Build();
        }
    }
}
