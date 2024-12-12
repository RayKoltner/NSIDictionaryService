using NSIDictionaryService.Data.Models;

namespace NSIDictionaryService.Api.Exceptions
{
    public class InvalidVersionException: Exception
    {
        public DictVersion Version { get; set; }

        public InvalidVersionException(string message, DictVersion version) : base(message) 
        {
            Version = version;
        }
    }
}
