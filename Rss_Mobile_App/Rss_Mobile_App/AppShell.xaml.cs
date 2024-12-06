namespace Rss_Mobile_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(Views.AccountDetailsPage), typeof(Views.AccountDetailsPage));
            Routing.RegisterRoute(nameof(Views.AuthorListPage), typeof(Views.AuthorListPage));
            Routing.RegisterRoute(nameof(Views.EpisodeDetailPage), typeof(Views.EpisodeDetailPage));
            Routing.RegisterRoute(nameof(Views.FeedDetailPage), typeof(Views.FeedDetailPage));
            Routing.RegisterRoute(nameof(Views.FeedListPage), typeof(Views.FeedListPage));
        }
    }
}
