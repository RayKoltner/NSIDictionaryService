using NSIDictionaryService.Api.Repositories.Common;
using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IDictCodeRepository: ICodeRepository<DictCode>
    {
    }

    public class DictCodeRepository: GenericCodeRepository<DictCode>, IDictCodeRepository
    {
        public DictCodeRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
    }
}
