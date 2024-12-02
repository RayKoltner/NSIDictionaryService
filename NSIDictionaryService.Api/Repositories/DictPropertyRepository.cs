using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories
{
    public class DictPropertyRepository : GenericRepository<DictProperty>
    {
        public DictPropertyRepository(ApplicationContext contextFactory) : base(contextFactory)
        {
        }
    }
}
