using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories
{
    public interface IUploadDictRepository : IRepository<UploadDict>
    {
    }

    public class UploadDictRepository : GenericRepository<UploadDict>, IUploadDictRepository
    {
        public UploadDictRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
