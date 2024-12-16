
using NSIDictionaryService.Share.Exceptions;
using System.Xml.Linq;

namespace NSIDictionaryService.Share.Helpers
{
    public static class SimpleXMLValidator
    {
        public static bool Validate(XDocument xmlDoc)
        {
            XElement? packet = xmlDoc.Element("packet");
            if (packet is null) throw new XMLValidationException("Отсутствует элемент packet");

            XElement? header = packet.Element("zglv");
            if (header is null) throw new XMLValidationException("Отсутствует элемент zglv");
            if (!header.Descendants().Any()) throw new XMLValidationException("Элемент zglv пустой");

            var entry = packet.Elements("zap");
            if (!entry.Any()) throw new XMLValidationException("Отсутствует элемент zap");
            return true;
        }
    }
}
