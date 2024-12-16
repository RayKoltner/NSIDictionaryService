using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Data;

namespace NSIDictionaryService.Api.Repositories.Dictionaries
{
    public interface IV012Repository : IRepository<V012Dictionary>
    {
    }

    public class V012Repository : GenericRepository<V012Dictionary>, IV012Repository
    {
        public V012Repository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
