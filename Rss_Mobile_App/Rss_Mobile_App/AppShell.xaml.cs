using Rss_Mobile_App.Views;

namespace Rss_Mobile_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AccountDetailsPage), typeof(AccountDetailsPage));
            Routing.RegisterRoute(nameof(AuthorDetailPage), typeof(AuthorDetailPage));
            Routing.RegisterRoute(nameof(AuthorListPage), typeof(AuthorListPage));
            Routing.RegisterRoute(nameof(EpisodeDetailPage), typeof(EpisodeDetailPage));
            Routing.RegisterRoute(nameof(FeedDetailPage), typeof(FeedDetailPage));
            Routing.RegisterRoute(nameof(FeedListPage), typeof(FeedListPage));
        }
    }
}
