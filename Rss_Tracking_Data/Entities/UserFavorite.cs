namespace Rss_Tracking_Data.Entities
{
    public class UserFavorite
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Creator Creator { get; set; }
        public Guid UserId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
