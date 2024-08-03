namespace eCommerceMicroservicesV2.Ordering.Domain.Exceptions;

public class DomainException : Exception
{
	public DomainException(string message)
		: base($"Domain Exception: {message} has been thrown from Domain Layer.")
	{

	}
}
