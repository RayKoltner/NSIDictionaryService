using NSIDictionaryService.Api.Repositories.Common;
using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IDictDependancyRepository : ICodeRepository<DictDependancy>
    {
    }

    public class DictDependancyRepository : GenericCodeRepository<DictDependancy>, IDictDependancyRepository
    {
        public DictDependancyRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
