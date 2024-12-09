namespace NSIDictionaryService.Api.Repositories.Common
{
    public interface ICodeRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetByKeyAsync(int id);
        T? GetByKey(int id);
    }
}
