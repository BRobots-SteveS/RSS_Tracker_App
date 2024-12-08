using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class AuthorDetailPage : UraniumUI.Pages.UraniumContentPage
{
    public AuthorDetailPage(AuthorDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
