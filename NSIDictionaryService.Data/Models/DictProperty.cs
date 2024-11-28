namespace NSIDictionaryService.Data.Models
{
    public class DictProperty: BaseEntity
    {
        public string PropertyName { get; set; } = String.Empty; //Name of the property in dict class

        public string PropertyCode {  get; set; } = String.Empty; //Name of the property in XML

        public string PropertyType { get; set; } = String.Empty;

        public string PropertyMethod {  get; set; } = String.Empty; //"" for string type
    }
}
