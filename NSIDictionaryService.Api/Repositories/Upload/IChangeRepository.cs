using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Repositories.Upload
{
    public interface IChangeRepository : IRepository<Change>, IDisposable
    {
    }

    public class ChangeRepository: GenericRepository<Change>, IChangeRepository
    {
        public ChangeRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
