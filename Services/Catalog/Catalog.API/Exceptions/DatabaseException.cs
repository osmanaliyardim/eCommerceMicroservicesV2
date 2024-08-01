namespace eCommerceMicroservicesV2.Catalog.API.Exceptions;

public class DatabaseException : InternalServerException
{
    public DatabaseException(string message) : base(message)
    {
        
    }

    public DatabaseException(string message, string details) : base(message, details)
    {

    }
}
