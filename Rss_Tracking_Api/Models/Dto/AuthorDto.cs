namespace Rss_Tracking_Api.Models.Dto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> FeedIds { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Uri { get; set; }
    }
}
