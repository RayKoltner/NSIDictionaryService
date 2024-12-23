namespace NSIDictionaryService.Api.Exceptions
{
    public class OutdatedDependencyException: Exception
    {
        public OutdatedDependencyException(string message) : base(message) { }
    }
}
