using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Data.Repositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        List<Author> GetAuthorsByName(string name);
        List<Author> GetAuthorsByEmail(string email);
        List<Author> GetAuthorsByUri(string uri);
        List<Author> GetAuthorsByFeedId(Guid feedId);
        FeedsAuthors AddFeedAuthor(Guid feedId, Guid authorId);
        bool DoesFeedAuthorExist(Guid feedId, Guid authorId);
    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(Rss_TrackingDbContext context) : base(context) { }
        public override Author? GetById(Guid id) => _context.Authors.Where(x => x.Id == id).FirstOrDefault();
        public override List<Author> GetAll() => _context.Authors.ToList();
        public override Author Add(Author author)
        {
            author.Id = Guid.NewGuid();
            return _context.Authors.Add(author).Entity;
        }
        public override Author Update(Author author) => _context.Authors.Update(author).Entity;
        public override void Delete(Author author) => _context.Authors.Remove(author);
        public override bool AlreadyExists(Author author) => _context.Authors.Any(x => x.Name ==  author.Name && x.Email == author.Email && x.Uri == author.Uri);
        public List<Author> GetAuthorsByName(string name) => _context.Authors.Where(x => x.Name == name).ToList();
        public List<Author> GetAuthorsByEmail(string email) => _context.Authors.Where(x => x.Email == email).ToList();
        public List<Author> GetAuthorsByUri(string uri) => _context.Authors.Where(x => x.Uri == uri).ToList();
        public List<Author> GetAuthorsByFeedId(Guid feedId) => _context.FeedsAuthors.Where(x => x.FeedId == feedId).Select(x => x.Author).ToList();
        public FeedsAuthors AddFeedAuthor(Guid feedId, Guid authorId) => _context.FeedsAuthors.Add(new() { Id = Guid.NewGuid(), FeedId = feedId, AuthorId = authorId }).Entity;
        public bool DoesFeedAuthorExist(Guid feedId, Guid authorid) => _context.FeedsAuthors.Any(x => x.FeedId == feedId && x.AuthorId == authorid);
    }
}
