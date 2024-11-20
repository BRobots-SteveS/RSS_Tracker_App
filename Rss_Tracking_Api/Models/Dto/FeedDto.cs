using Rss_Tracking_Lib.Enums;

namespace Rss_Tracking_Api.Models.Dto
{
    public class FeedDto
    {
        private Platform _platform = Rss_Tracking_Lib.Enums.Platform.Basic;
        public string Platform { get => _platform.ToString(); set => _platform = Enum.Parse<Platform>(value); }
        public required Guid Id;
        public required IEnumerable<Guid> AuthorId;
        public string CreatorId = string.Empty;
        public string Description = string.Empty;
        public string FeedUri = string.Empty;
        public string? ThumbnailUri;
        public DateTimeOffset PublishedTime;
    }
}