using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using System.Xml.Linq;

namespace NSIDictionaryService.Api.Services
{
    public class UniversalXMLDocCreator
    {
        private readonly IDictPropertyRepository _repository;

        public UniversalXMLDocCreator(IDictPropertyRepository propertyRepository)
        {
            _repository = propertyRepository;
        }

        public XDocument CreateDocument<T>(List<BaseDictionaryType<T>> data, DictVersion version, string identifier)
        {
            if (!data.Any()) throw new Exception("Записи отсутствуют");
            var dictType = data.FirstOrDefault().GetType();

            var properties = _repository.FindBy(x => x.DictCodeId == version.DictCodeId);
            if (!properties.Any()) throw new Exception("Отстутствуют записи об атрибутах такого словаря");

            XDocument doc = new XDocument(new XDeclaration("1.0", "windows-1251", null));

            XElement packet = new XElement("packet");

            XElement header = new XElement("zglv");
            header.Add(new XElement("type", identifier));
            header.Add(new XElement("version", version.VersionCode.ToString()));
            header.Add(new XElement("date", version.PublicationDate.ToShortDateString()));

            packet.Add(header);

            foreach (BaseDictionaryType<T> entry in data)
            {
                XElement zap = new XElement("zap");
                foreach(var property in properties)
                {
                    zap.Add(new XElement(property.PropertyCode,
                        dictType.GetProperty(property.PropertyName).GetValue(entry).ToString())); // To make this truly universal
                }
                packet.Add(zap);
            }

            doc.Add(packet);

            return doc;
        }
    }
}
