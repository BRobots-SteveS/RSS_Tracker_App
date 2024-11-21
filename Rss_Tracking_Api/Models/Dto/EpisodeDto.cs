namespace Rss_Tracking_Api.Models.Dto
{
    public class EpisodeDto
    {
        public Guid Id { get; set; }
        public Guid FeedId { get; set; }
        public string? EpisodeId { get; set; }
        public string? EpisodeName { get; set; }
        public string? PreviewUrl { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}