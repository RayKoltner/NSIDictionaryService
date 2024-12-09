
using Microsoft.EntityFrameworkCore;
using NSIDictionaryService.Data;

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
