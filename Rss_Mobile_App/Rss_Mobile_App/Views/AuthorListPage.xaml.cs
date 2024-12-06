using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class AuthorListPage : UraniumUI.Pages.UraniumContentPage
{
    public AuthorListPage(AuthorListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
