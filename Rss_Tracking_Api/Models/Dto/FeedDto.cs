using Rss_Tracking_Lib.Enums;

namespace Rss_Tracking_Api.Models.Dto
{
    public class FeedDto
    {
        private Platform _platform = Rss_Tracking_Lib.Enums.Platform.Basic;
        public string Platform { get => _platform.ToString(); set => _platform = Enum.Parse<Platform>(value); }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string? AuthorEmail { get; set; }
        public string? AuthorUri { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FeedUri { get; set; } = string.Empty;
        public string? ThumbnailUri { get; set; }
        public DateTime PublishedTime { get; set; }
    }
}