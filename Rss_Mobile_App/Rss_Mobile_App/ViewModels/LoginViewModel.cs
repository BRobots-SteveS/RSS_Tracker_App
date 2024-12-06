using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Mobile_App.Views;
using Rss_Tracking_App.Models.Dto;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly UserRepository _repo;
        public LoginViewModel(INavigationService navigation, IDialogService dialogService, UserRepository repo) : base(navigation, dialogService)
        {
            _repo = repo;
            newUser = new();
            existingUser = new();
        }

        [ObservableProperty]
        private UserDto newUser;
        [ObservableProperty]
        private UserDto existingUser;

        [RelayCommand]
        public async void Login()
        {
            try
            {
                var user = await _repo.DoLogin(ExistingUser.Username, ExistingUser.Password);
                if (user == null) throw new Exception("Login failed");
                Preferences.Set("user", user.Id.ToString());
                await Navigation.NavigateToAsync(nameof(AccountDetailsPage));
            }
            catch (HttpRequestException ex) { await DialogService.ConfirmAsync("Login failed - server", $"{ex.StatusCode} - {ex.Message}"); }
            catch (Exception ex) { await DialogService.ConfirmAsync("Unknown error", ex.Message); }
        }
        [RelayCommand]
        public async void Register()
        {
            try
            {
                var user = await _repo.DoRegister(NewUser.Username, NewUser.Password);
                if (user == null) throw new Exception("Register failed");
                Preferences.Set("user", user.Id.ToString());
                await Navigation.NavigateToAsync(nameof(AccountDetailsPage));
            }
            catch (HttpRequestException ex) { await DialogService.ConfirmAsync("Register failed - server", $"{ex.StatusCode} - {ex.Message}"); }
            catch (Exception ex) { await DialogService.ConfirmAsync("Unknown error", ex.Message); }
        }
    }
}
