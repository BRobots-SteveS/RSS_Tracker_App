using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Data.Repositories
{
    public partial interface IBaseRepository<T> where T : class
    {
        T? GetById(Guid id);
        List<T> GetAll();
        T? Add(T entity);
        T? Update(T entity);
        void Delete(T entity);
        bool AlreadyExists(T entity);
        void SaveChanges();
    }
    public partial class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        internal readonly Rss_TrackingDbContext _context;
        public BaseRepository(Rss_TrackingDbContext context) { _context = context; }

        public virtual T? Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool AlreadyExists(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T? Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
