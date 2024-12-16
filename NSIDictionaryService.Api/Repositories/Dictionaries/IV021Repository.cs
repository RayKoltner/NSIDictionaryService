using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Api.Repositories.Dictionaries
{
    public interface IV021Repository: IRepository<V021Dictionary>
    {
    }

    public class V021Repository: GenericRepository<V021Dictionary>, IV021Repository
    {
        public V021Repository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
    }
}
