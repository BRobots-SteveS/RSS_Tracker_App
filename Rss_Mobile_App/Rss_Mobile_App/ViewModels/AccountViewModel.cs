using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;

namespace Rss_Mobile_App.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {
        [ObservableProperty]
        public UserDto user;
        public AccountViewModel(INavigationService navigation) : base(navigation)
        { }
    }
}
