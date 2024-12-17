using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Mobile_App.Views;
using Rss_Tracking_App.Models.Dto;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {
        private readonly IUserRepository _repo;
        [ObservableProperty]
        private UserDto user;
        [ObservableProperty]
        private string newPassword;
        public AccountViewModel(INavigationService navigation, IDialogService dialogService, IUserRepository repo) : base(navigation, dialogService)
        { 
            _repo = repo;
            newPassword = string.Empty;
            var userId = Preferences.Get("user", Guid.Empty.ToString());
            User = new() { Id = Guid.Parse(userId) };
        }

        [RelayCommand]
        public async Task ReloadData() => User = await _repo.GetRowById(Guid.Parse(Preferences.Get("user", Guid.Empty.ToString())));

        [RelayCommand]
        public async Task ChangePassword()
        {
            User.Password = Convert.ToHexString(System.Text.Encoding.UTF8.GetBytes(NewPassword));
            await _repo.UpdateRow(User, User.Id);
            await DoLogout();
        }

        [RelayCommand]
        public async Task ToAccountDetails() => await GoToAccountDetails();
        [RelayCommand]
        public async Task ToFeedList() => await GoToFeedList();
        [RelayCommand]
        public async Task ToAuthorList() => await GoToAuthorList();
        [RelayCommand]
        public async Task ToFavorites() => await GoToFavorites();
    }
}
