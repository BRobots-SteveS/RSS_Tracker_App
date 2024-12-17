using Rss_Mobile_App.Services;
using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class AccountDetailsPage : UraniumUI.Pages.UraniumContentPage
{
    private INavigationService navigationService;
    public AccountDetailsPage(AccountViewModel vm, INavigationService navigationService)
    {
        InitializeComponent();
        BindingContext = vm;
        this.navigationService = navigationService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await navigationService.NavigateToAsync(nameof(AuthorListPage));
    }
}
