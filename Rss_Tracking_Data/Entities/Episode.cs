namespace Rss_Tracking_Data.Entities
{
    public class Episode
    {
        public Guid Id { get; set; }
        public string EpisodeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? ThumbnailUrl { get; set; }
        public DateTime Published { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Duration { get; set; } = 0;
        public string? EpisodeType { get; set; }
        public bool IsExplicit { get; set; } = false;
        public Guid CreatorId { get; set; }
    }
}
