namespace Rss_Tracking_Api.Models.Dto
{
    public class AuthorDto
    {
        public required Guid Id { get; set; }
        public required IEnumerable<Guid> FeedIds { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Uri { get; set; }
    }
}
