namespace NSIDictionaryService.Api.Exceptions
{
    public class OutdatedDependancyException: Exception
    {
        public OutdatedDependancyException(string message) : base(message) { }
    }
}
