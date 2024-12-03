using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class EpisodeDto : ObservableObject
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private Guid feedId;
        [ObservableProperty]
        private string? episodeId;
        [ObservableProperty]
        private string? episodeName;
        [ObservableProperty]
        private string? previewUrl;
        [ObservableProperty]
        private string? description;
        [ObservableProperty]
        private DateTimeOffset createdOn;
    }
}