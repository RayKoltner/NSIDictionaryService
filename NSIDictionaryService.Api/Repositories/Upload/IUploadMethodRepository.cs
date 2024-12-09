using NSIDictionaryService.Api.Repositories.Common;
using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IUploadMethodRepository: ICodeRepository<UploadMethod>
    {
    }

    public class UploadMethodRepository : GenericCodeRepository<UploadMethod>, IUploadMethodRepository
    {
        public UploadMethodRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
