namespace Rss_Tracking_Data.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Uri { get; set; }
    }
}
