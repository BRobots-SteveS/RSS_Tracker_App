using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {
        private readonly UserRepository _repo;
        [ObservableProperty]
        private UserDto user;
        public AccountViewModel(INavigationService navigation, IDialogService dialogService, UserRepository repo) : base(navigation, dialogService)
        { 
            _repo = repo;
            var userId = Preferences.Get("user", Guid.Empty.ToString());
            user = _repo.GetRowById(Guid.Parse(userId)).GetAwaiter().GetResult();
        }

        [RelayCommand]
        public async void ChangePassword()
        {
            user.Password = Convert.ToHexString(System.Text.Encoding.UTF8.GetBytes(user.Password));
            await _repo.UpdateRow(User, User.Id);
        }
    }
}
