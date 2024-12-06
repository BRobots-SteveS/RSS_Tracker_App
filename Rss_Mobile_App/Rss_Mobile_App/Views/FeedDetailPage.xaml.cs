using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class FeedDetailPage : UraniumUI.Pages.UraniumContentPage
{
    public FeedDetailPage(FeedDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
