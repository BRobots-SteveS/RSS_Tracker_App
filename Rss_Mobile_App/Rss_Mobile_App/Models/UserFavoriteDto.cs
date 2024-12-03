using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class UserFavoriteDto : ObservableObject
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private UserDto user;
        [ObservableProperty]
        private FeedDto feed;
        [ObservableProperty]
        private AuthorDto? author;
    }
}
