namespace eCommerceMicroservicesV2.Ordering.Application.Exceptions;

public class DatabaseException : InternalServerException
{
    public DatabaseException(string message) : base(message)
    {
        
    }

    public DatabaseException(string message, string details) : base(message, details)
    {
        
    }
}
