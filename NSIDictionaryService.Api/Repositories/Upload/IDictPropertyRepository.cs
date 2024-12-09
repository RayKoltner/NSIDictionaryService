using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories
{
    public interface IDictPropertyRepository : IRepository<DictProperty>, IDisposable
    {
    }
    public class DictPropertyRepository : GenericRepository<DictProperty>, IDictPropertyRepository
    {
        public DictPropertyRepository(ApplicationContext contextFactory) : base(contextFactory)
        {
        }
    }
}
