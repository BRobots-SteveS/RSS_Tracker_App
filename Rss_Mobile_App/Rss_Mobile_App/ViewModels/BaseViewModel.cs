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

        [ObservableProperty]
        private bool isRefreshing = false;
        public BaseViewModel(INavigationService navigation, IDialogService dialogService)
        {
            Navigation = navigation;
            DialogService = dialogService;
        }

        protected void ToggleRefreshing() => IsRefreshing = !IsRefreshing;
        public virtual async Task DoRefresh() { }

        [RelayCommand]
        protected async Task ReloadData()
        {
            ToggleRefreshing();
            await DoRefresh();
            ToggleRefreshing();
        }
        [RelayCommand]
        protected async Task DoLogout()
        {
            Preferences.Remove("user");
            await Navigation.NavigateToAsync($"{nameof(LoginPage)}");
        }
        [RelayCommand]
        protected async Task GoToAccountDetails()
        { await Navigation.NavigateToAsync(nameof(AccountDetailsPage), new Dictionary<string, object> { { "user", Guid.Parse(Preferences.Get("user", Guid.Empty.ToString())) } }); }
        [RelayCommand]
        protected async Task GoToFavorites()
        { await Navigation.NavigateToAsync(nameof(FeedListPage), new Dictionary<string, object> { { "user", Guid.Parse(Preferences.Get("user", Guid.Empty.ToString())) } }); }
        [RelayCommand]
        protected async Task GoToFeedList() => await Navigation.NavigateToAsync(nameof(FeedListPage));
        [RelayCommand]
        protected async Task GoToAuthorList() => await Navigation.NavigateToAsync(nameof(AuthorListPage));
        [RelayCommand]
        protected async Task OpenBrowser(string uri)
        {
            if(string.IsNullOrEmpty(uri)) return;
            await Navigation.OpenBrowser(uri);
        }

        [RelayCommand]
        protected async Task OpenEmail(string route)
        {
            if(string.IsNullOrEmpty(route)) return;
            await Navigation.OpenBrowser($"mailto:{route}");
        }
    }
}
