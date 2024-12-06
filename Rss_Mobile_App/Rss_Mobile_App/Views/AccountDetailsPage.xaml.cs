using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class AccountDetailsPage : UraniumUI.Pages.UraniumContentPage
{
    public AccountDetailsPage(AccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
