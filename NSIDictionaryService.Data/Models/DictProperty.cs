namespace NSIDictionaryService.Data.Models
{
    public class DictProperty: BaseEntity
    {
        public int DictCodeId { get; set; } //Name of the dictionary itself
        public virtual DictCode? DictCode { get; private set; }

        public string PropertyName { get; set; } = String.Empty; //Name of the property in dict class

        public string PropertyCode {  get; set; } = String.Empty; //Name of the property in XML
    }
}
