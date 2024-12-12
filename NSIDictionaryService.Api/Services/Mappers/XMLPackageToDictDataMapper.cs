using NSIDictionaryService.Share.DTOs;
using System.Xml.Linq;

namespace NSIDictionaryService.Api.Services
{
    public static class XMLPackageToDictDataMapper
    {
        public static DictionaryVersionDataDTO GetVersion(XDocument rawPackage, string dictIdentifier)
        {
            DictionaryVersionDataDTO version = new DictionaryVersionDataDTO();

            XElement header = rawPackage.Element("packet").Element("zglv"); //Assuming the XDocument is valid

            version.Version = header.Element("version").Value;
            version.CreateDate = DateOnly.Parse(header.Element("date").Value);

            return version;
        }

        public static DictionaryDataDTO GetData(XDocument rawPackage)
        {
            DictionaryDataDTO data = new DictionaryDataDTO();
            data.ResponseData = new List<List<JsonPair>>();

            XElement package = rawPackage.Element("packet"); //Assuming the XDocument is valid

            foreach (XElement entry in package.Elements("zap"))
            {
                List<JsonPair> pairs = new List<JsonPair>();
                foreach (XElement pair in entry.Elements())
                {
                    pairs.Add(new JsonPair()
                    {
                        Column = pair.Name.ToString(),
                        Value = pair.Value
                    });
                }
                data.ResponseData.Add(pairs);
            }

            return data;
        }
    }
}
