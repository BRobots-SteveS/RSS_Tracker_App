using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Services;
using Rss_Mobile_App.Views;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public readonly INavigationService Navigation;
        public readonly IDialogService DialogService;
        public BaseViewModel(INavigationService navigation, IDialogService dialogService)
        {
            Navigation = navigation;
            DialogService = dialogService;
        }

        protected async Task DoLogout()
        {
            Preferences.Remove("user");
            await Navigation.NavigateToAsync($"///{nameof(LoginPage)}");
        }

        protected async Task GoToAccountDetails()
        { await Navigation.NavigateToAsync(nameof(AccountDetailsPage), new Dictionary<string, object> { { "user", Guid.Parse(Preferences.Get("user", Guid.Empty.ToString())) } }); }
        protected async Task GoToFeedList()
        { await Navigation.NavigateToAsync(nameof(FeedListPage)); }
        protected async Task GoToAuthorList()
        { await Navigation.NavigateToAsync(nameof(AuthorListPage)); }
        protected async Task GoToFavorites()
        { await Navigation.NavigateToAsync(nameof(FeedListPage), new Dictionary<string, object> { { "user", Guid.Parse(Preferences.Get("user", Guid.Empty.ToString())) } }); }
    }
}
