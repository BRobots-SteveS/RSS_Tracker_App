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

        [RelayCommand]
        public async Task DoLogout()
        {
            Preferences.Remove("user");
            await Navigation.NavigateToAsync($"///{nameof(LoginPage)}");
        }
    }
}
