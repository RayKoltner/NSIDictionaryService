using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Api.Repositories.Dictionaries
{
    public interface IV025Repository: IRepository<V025Dictionary>
    {
    }

    public class V025Repository : GenericRepository<V025Dictionary>, IV025Repository
    {
        public V025Repository(ApplicationContext context) : base(context)
        {

        }
    }
}
