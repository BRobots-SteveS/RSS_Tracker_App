namespace Rss_Tracking_Data.Entities
{
    public class Feed
    {
        public Guid Id { get; set; }
        public Enums.Platform Platform { get; set; } = Enums.Platform.Basic;
        public Enums.YoutubeType YoutubeType { get; set; } = Enums.YoutubeType.None;
        public string? ChannelId { get; set; }
        public string? PlaylistId { get; set; }
        public string? ImageUrl { get; set; }
        public required string FeedUrl { get; set; }
        public required string CreatorId { get; set; }
        public required string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
