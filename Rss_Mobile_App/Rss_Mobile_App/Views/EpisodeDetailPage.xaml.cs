using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class EpisodeDetailPage : UraniumUI.Pages.UraniumContentPage
{
    public EpisodeDetailPage(EpisodeDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
