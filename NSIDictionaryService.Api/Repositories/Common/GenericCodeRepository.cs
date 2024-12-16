using Microsoft.EntityFrameworkCore;
using NSIDictionaryService.Data;
using System.Linq.Expressions;

namespace NSIDictionaryService.Api.Repositories.Common
{
    public class GenericCodeRepository<T> : ICodeRepository<T>, IDisposable where T : class
    {
        protected readonly ApplicationContext _сontextFactory;
        private bool _disposedValue;
        protected GenericCodeRepository(ApplicationContext contextFactory)
        {
            _сontextFactory = contextFactory;
        }
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _сontextFactory.Set<T>();
            return query;
        }

        public T? GetByKey(int id)
        {
            var query = _сontextFactory.Set<T>().Find(id);
            return query;
        }

        public async Task<T?> GetByKeyAsync(int id)
        {
            return await _сontextFactory.Set<T>().FindAsync(id);
        }

        public virtual T? First(Expression<Func<T, bool>> predicate)
        {
            return _сontextFactory.Set<T>().FirstOrDefault(predicate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _сontextFactory.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
