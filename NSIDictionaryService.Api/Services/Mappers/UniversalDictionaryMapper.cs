using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services
{
    public static class UniversalDictionaryMapper
    {
        public static BaseDictionaryType<T> FillWithData<T>(
            List<JsonPair> data, 
            BaseDictionaryType<T> dictionary, 
            List<DictProperty> properties) // TODO : Make this method foolproof
        {
            foreach (var property in properties)
            {
                string rawData = data.Where(x => x.Column == property.PropertyCode).FirstOrDefault().Value;

                Type propertyType = dictionary.GetType().GetProperty(property.PropertyName).PropertyType;
                propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                object result;
                if (propertyType == typeof(string))
                {
                    result = rawData;
                }
                else
                {
                    var parseMethod = propertyType.GetMethod("Parse", new Type[] { typeof(string) }); // Json only contains strings
                    try
                    {
                        result = parseMethod.Invoke(null, new object[] { rawData });
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        //Console.WriteLine(rawData);
                        if (Nullable.GetUnderlyingType(propertyType) != null)
                        {
                            result = null;
                        }
                        else
                        {
                            result = Activator.CreateInstance(propertyType);
                        }
                    }
                }
                Convert.ChangeType(result, propertyType);
                dictionary.GetType().GetProperty(property.PropertyName).SetValue(dictionary, result);
            }
            return dictionary;
        }
    }
}
