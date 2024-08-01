public class Messages
{
    // Const Errors
    public const string DEFAULT_ERROR = "ERROR - Something went wrong.";
    public const string VALIDATION_ERROR = "ERROR - Validation error occurred.";

    // Functional Errors
    public static string GetPerformanceError<TRequest>(int timeTaken)
    {
        return $"[PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken} seconds.";
    }
    public static string GetCustomException(string errorMessage, DateTime occuranceTime)
    {
        return $"Error Message: {errorMessage}, Time of occurrence {occuranceTime}";
    }
}
