using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class UserDto : ObservableObject
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;
    }
}
