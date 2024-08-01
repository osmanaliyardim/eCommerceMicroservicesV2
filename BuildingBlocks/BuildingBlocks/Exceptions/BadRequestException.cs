namespace eCommerceMicroservices2.BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public string? Details { get; set; }

    public BadRequestException(string message) : base(message)
    {
        
    }

    public BadRequestException(string mesage, string details) : base(mesage)
    {
        Details = details;
    }
}
