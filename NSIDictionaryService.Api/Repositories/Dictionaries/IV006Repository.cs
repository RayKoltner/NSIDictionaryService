using NSIDictionaryService.Data;
using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Api.Repositories.Dictionaries
{
    public interface IV006Repository: IRepository<V006Dictionary>
    {
    }

    public class V006Repository : GenericRepository<V006Dictionary>, IV006Repository
    {
        public V006Repository(ApplicationContext applicationContext) : base(applicationContext) 
        {
        }
    }
}
