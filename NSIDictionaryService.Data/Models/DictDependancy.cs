namespace NSIDictionaryService.Data.Models
{
    public class DictDependancy
    {
        public int Id { get; set; }

        public int DictId { get; set; }

        public virtual DictCode? DictCode { get; set; }

        public int DependancyId { get; set; }

        public virtual DictCode? DependancyCode { get; set; }
    }
}
