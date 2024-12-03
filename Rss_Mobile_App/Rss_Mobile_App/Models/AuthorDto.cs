using CommunityToolkit.Mvvm.ComponentModel;

namespace Rss_Tracking_App.Models.Dto
{
    public partial class AuthorDto : ObservableObject
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private IEnumerable<Guid> feedIds;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string? email;
        [ObservableProperty]
        private string? uri;
    }
}
