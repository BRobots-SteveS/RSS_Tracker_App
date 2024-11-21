using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Data.Repositories
{
    public interface IAuthorRepository
    {
        Author? GetAuthorById(Guid id);
        List<Author> GetAllAuthors();
        List<Author> GetAuthorsByName(string name);
        List<Author> GetAuthorsByEmail(string email);
        List<Author> GetAuthorsByUri(string uri);
        List<Author> GetAuthorsByFeedId(Guid feedId);
        Author AddAuthor(Author author);
        FeedsAuthors AddFeedAuthor(Guid feedId, Guid authorId);
        Author UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        bool DoesAuthorExist(Author author);
        bool DoesFeedAuthorExist(Guid feedId, Guid authorId);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public AuthorRepository(Rss_TrackingDbContext context) { _context = context; }
        public Author? GetAuthorById(Guid id) => _context.Authors.Where(x => x.Id == id).FirstOrDefault();
        public List<Author> GetAllAuthors() => _context.Authors.ToList();
        public List<Author> GetAuthorsByName(string name) => _context.Authors.Where(x => x.Name == name).ToList();
        public List<Author> GetAuthorsByEmail(string email) => _context.Authors.Where(x => x.Email == email).ToList();
        public List<Author> GetAuthorsByUri(string uri) => _context.Authors.Where(x => x.Uri == uri).ToList();
        public List<Author> GetAuthorsByFeedId(Guid feedId) => _context.FeedsAuthors.Where(x => x.FeedId == feedId).Select(x => x.Author).ToList();
        public Author AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            return _context.Authors.Add(author).Entity;
        }
        public FeedsAuthors AddFeedAuthor(Guid feedId, Guid authorId) => _context.FeedsAuthors.Add(new() { Id = Guid.NewGuid(), FeedId = feedId, AuthorId = authorId }).Entity;
        public Author UpdateAuthor(Author author) => _context.Authors.Update(author).Entity;
        public void DeleteAuthor(Author author) => _context.Authors.Remove(author);
        public bool DoesAuthorExist(Author author) => _context.Authors.Any(x => x.Name ==  author.Name && x.Email == author.Email && x.Uri == author.Uri);
        public bool DoesFeedAuthorExist(Guid feedId, Guid authorid) => _context.FeedsAuthors.Any(x => x.FeedId == feedId && x.AuthorId == authorid);
    }
}
