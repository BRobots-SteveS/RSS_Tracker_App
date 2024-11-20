namespace Rss_Tracking_Api.Models.Dto
{
    public class UserFavoriteDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; }
        public FeedDto Creator { get; set; }
    }
}
