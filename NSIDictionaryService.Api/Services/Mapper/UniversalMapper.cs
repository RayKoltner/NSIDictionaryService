using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services
{
    public class UniversalMapper
    {
        public UniversalMapper() { }

        public BaseDictionaryType FillWithData(List<JsonPair> data, BaseDictionaryType dictionary, string identifier) // TODO : Make this method foolproof
        {
            List<DictProperty> properties = new List<DictProperty>(); //Add repository for retriving DictProperties
            
            foreach (var property in properties)
            {
                string rawData = data.Where(x => x.Column == property.PropertyCode).FirstOrDefault().Value;

                Type propertyType = Type.GetType(property.PropertyType);
                object result = null;
                if (property.PropertyMethod.Length != 0)
                {
                    var parseMethod = propertyType.GetMethod(property.PropertyMethod, new Type[] { typeof(string) }); // XML only contains strings
                    result = parseMethod.Invoke(null, new object[] { rawData });
                }
                else
                {
                    result = rawData;
                }
                Convert.ChangeType(result, propertyType);
                dictionary.GetType().GetProperty(property.PropertyName).SetValue(dictionary, result);
            }

            return dictionary;
        }
    }
}
