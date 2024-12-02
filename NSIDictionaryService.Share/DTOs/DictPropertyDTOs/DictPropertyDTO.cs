namespace NSIDictionaryService.Share.DTOs
{
    public class DictPropertyDTO
    {
        public string DictionaryName { get; set; } = String.Empty; //Name of the dictionary itself

        public string PropertyName { get; set; } = String.Empty; //Name of the property in dict class

        public string PropertyCode { get; set; } = String.Empty; //Name of the property in XML
    }
}
