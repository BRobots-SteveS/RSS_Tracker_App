namespace Rss_Tracking_Api.Models.Dto
{
    public class UserFavoriteDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; }
        public FeedDto Feed { get; set; }
        public AuthorDto? Author { get; set; }
    }
}
