using NSIDictionaryService.Api.Repositories.Common;
using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IDictDependencyRepository : ICodeRepository<DictDependency>
    {
    }

    public class DictDependencyRepository : GenericCodeRepository<DictDependency>, IDictDependencyRepository
    {
        public DictDependencyRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
