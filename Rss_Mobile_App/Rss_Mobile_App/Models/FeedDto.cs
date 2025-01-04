using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class FeedDto : ObservableObject
    {
        public FeedDto()
        {
            id = Guid.Empty;
            platform = "Basic";
            title = string.Empty;
            authorName = string.Empty;
            authorEmail = string.Empty;
            AuthorUri = string.Empty;
            creatorId = string.Empty;
            description = string.Empty;
            FeedUri = string.Empty;
            thumbnailUri = string.Empty;
            publishedTime = DateTime.Now;
        }
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