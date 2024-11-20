namespace Rss_Tracking_Data.Entities
{
    public class FeedsAuthors
    {
        public Guid Id { get; set; }
        public Feed Feed { get; set; }
        public Author Author { get; set; }
        public Guid FeedId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
