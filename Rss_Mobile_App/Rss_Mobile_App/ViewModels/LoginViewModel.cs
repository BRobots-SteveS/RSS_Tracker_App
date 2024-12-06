using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;

namespace Rss_Mobile_App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigation) : base(navigation)
        {
            user = new();
        }

        [ObservableProperty]
        private UserDto user;

        [RelayCommand]
        public void GoToRegister()
        {
            Navigation.NavigateToAsync(nameof(RegisterViewModel));
        }
    }
}
