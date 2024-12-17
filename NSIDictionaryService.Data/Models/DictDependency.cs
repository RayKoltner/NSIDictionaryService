namespace NSIDictionaryService.Data.Models
{
    public class DictDependency
    {
        public int Id { get; set; }

        public int DictId { get; set; }

        public virtual DictCode? DictCode { get; set; }

        public int DependencyId { get; set; }

        public virtual DictCode? DependencyCode { get; set; }
    }
}
