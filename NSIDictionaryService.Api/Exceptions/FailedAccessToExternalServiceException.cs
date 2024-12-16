namespace NSIDictionaryService.Api.Exceptions
{
    public class FailedAccessToExternalServiceException: Exception
    {
        public FailedAccessToExternalServiceException(string message) : base(message) { }
    }
}
