namespace eCommerceMicroservices2.BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public NotFoundException(string name, object key) : base($"({name.ToUpper()}) entity for ({key}) was not found")
    {
        
    }
}
