using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class FeedListPage : UraniumUI.Pages.UraniumContentPage
{
    public FeedListPage(FeedListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
