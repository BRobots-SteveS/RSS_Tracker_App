namespace Rss_Tracking_Data.Entities
{
    public class CreatorsAuthors
    {
        public Guid Id { get; set; }
        public Creator Creator { get; set; }
        public Author Author { get; set; }
        public Guid CreatorId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
