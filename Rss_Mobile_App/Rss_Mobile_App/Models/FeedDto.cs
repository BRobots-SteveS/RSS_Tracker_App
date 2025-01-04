using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class FeedDto : ObservableObject
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private string platform;
        [ObservableProperty] 
        private string title;
        [ObservableProperty]
        private string authorName;
        [ObservableProperty]
        private string? authorEmail;
        [ObservableProperty]
        private string? authorUri;
        [ObservableProperty]
        private string creatorId;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string feedUri;
        [ObservableProperty]
        private string? thumbnailUri;
        [ObservableProperty]
        private DateTime publishedTime;
    }
}