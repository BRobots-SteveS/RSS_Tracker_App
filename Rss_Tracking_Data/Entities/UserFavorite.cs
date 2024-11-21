namespace Rss_Tracking_Data.Entities
{
    public class UserFavorite
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Feed Feed { get; set; }
        public Author? Author { get; set; }
        public Guid UserId { get; set; }
        public Guid FeedId { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
