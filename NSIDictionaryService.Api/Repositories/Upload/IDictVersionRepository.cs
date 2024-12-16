using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories
{
    public interface IDictVersionRepository : IRepository<DictVersion>
    {
    }

    public class DictVersionRepository : GenericRepository<DictVersion>, IDictVersionRepository
    {
        public DictVersionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
