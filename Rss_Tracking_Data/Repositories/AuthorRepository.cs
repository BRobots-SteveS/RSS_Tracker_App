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
        Author UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
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
        public Author AddAuthor(Author author) => _context.Authors.Add(author).Entity;
        public Author UpdateAuthor(Author author) => _context.Authors.Update(author).Entity;
        public void DeleteAuthor(Author author) => _context.Authors.Remove(author);
    }
}
