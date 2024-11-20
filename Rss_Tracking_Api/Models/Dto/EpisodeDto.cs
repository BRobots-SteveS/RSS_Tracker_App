namespace Rss_Tracking_Api.Models.Dto
{
    public class EpisodeDto
    {
        public Guid Id;
        public Guid FeedId;
        public string? EpisodeId;
        public string? EpisodeName;
        public string? PreviewUrl;
        public string? Description;
        public DateTimeOffset CreatedOn;
    }
}