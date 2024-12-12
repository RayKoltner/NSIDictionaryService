using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories
{
    public interface IUploadInfoRepository : IRepository<UploadInfo>
    {
    }

    public class UploadInfoRepository : GenericRepository<UploadInfo>, IUploadInfoRepository
    {
        public UploadInfoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
