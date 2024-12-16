using NSIDictionaryService.Api.Repositories.Common;
using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IUploadResultRepository : ICodeRepository<UploadResult>
    {
    }

    public class UploadResultRepository: GenericCodeRepository<UploadResult>, IUploadResultRepository
    {
        public UploadResultRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
