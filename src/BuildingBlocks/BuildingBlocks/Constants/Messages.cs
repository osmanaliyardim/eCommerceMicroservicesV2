public class Messages
{
    // Const Errors
    public const string DEFAULT_ERROR = "ERROR - Something went wrong.";
    public const string VALIDATION_ERROR = "ERROR - Validation error occurred.";
    public const string DATABASE_ERROR = "ERROR - Problem with database saving.";

    // Functional Errors
    public static string GetPerformanceError<TRequest>(int timeTaken)
    {
        return $"[PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken} seconds.";
    }
    public static string GetCustomException(string errorMessage, DateTime occuranceTime)
    {
        return $"Error Message: {errorMessage}, Time of occurrence {occuranceTime}";
    }

    // Const Infos
    public const string NO_COUPON_FOUND = "INFO - No coupon found, there will be no discount.";
    public const string COUPON_FOUND = "INFO - Coupon found, discount(s) will be applied to your total price.";

    // Connection Strings/Keys/Secrets
    public const string CATALOG_DB_NAME = "CatalogDB";
    public const string BASKET_DB_NAME = "BasketDB";
    public const string REDIS_CACHE_NAME = "RedisCacheConn";
    public const string DISCOUNT_DB_NAME = "DiscountDB";
    public const string DISCOUNT_GRPC_NAME = "GrpcSettings:DiscountUrl";
    public const string ORDERING_DB_NAME = "OrderingDB";

    // Endpoints
    public const string HEALTH_CHECK_ENDPOINT = "/health";
}
