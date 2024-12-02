using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services
{
    public static class UniversalDictionaryMapper
    {
        public static BaseDictionaryType FillWithData(
            List<JsonPair> data, 
            BaseDictionaryType dictionary, 
            List<DictProperty> properties) // TODO : Make this method foolproof
        {            
            foreach (var property in properties)
            {
                string rawData = data.Where(x => x.Column == property.PropertyCode).FirstOrDefault().Value;

                Type propertyType = dictionary.GetType().GetProperty(property.PropertyName).GetType();
                object result = null;
                if (propertyType == typeof(string))
                {
                    result = rawData;
                }
                else
                {
                    var parseMethod = propertyType.GetMethod("Parse", new Type[] { typeof(string) }); // Json only contains strings
                    result = parseMethod.Invoke(null, new object[] { rawData });
                }
                Convert.ChangeType(result, propertyType);
                dictionary.GetType().GetProperty(property.PropertyName).SetValue(dictionary, result);
            }
            return dictionary;
        }
    }
}
