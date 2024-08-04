namespace eCommerceMicroservicesV2.BuildingBlocks.Constants;

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
    public static string GetDomainEventMessage(string domainEventName)
    {
        return $"Domain Event handled: {domainEventName}";
    }
    public static string GetIntegrationEventMessage(string integrationEventName)
    {
        return $"Integration Event handled: {integrationEventName}";
    }
    public static string GetDomainEventFailedMessage(string integrationEventName)
    {
        return $"Integration Event failed: {integrationEventName}";
    }

    // Const Infos
    public const string NO_COUPON_FOUND = "INFO - No coupon found, there will be no discount.";
    public const string COUPON_FOUND = "INFO - Coupon found, discount(s) will be applied to your total price.";
    public const string DOMAIN_EVENT_HANDLEND = "INFO - Domain Event handled successfully.";

    // Connection Strings/Keys/Secrets
    public const string CATALOG_DB_NAME = "CatalogDB";
    public const string BASKET_DB_NAME = "BasketDB";
    public const string REDIS_CACHE_NAME = "RedisCacheConn";
    public const string DISCOUNT_DB_NAME = "DiscountDB";
    public const string DISCOUNT_GRPC_NAME = "GrpcSettings:DiscountUrl";
    public const string ORDERING_DB_NAME = "OrderingDB";
    public const string ORDER_FULLFILMENT_FLAG = "OrderFullfilment";

    // Endpoints
    public const string HEALTH_CHECK_ENDPOINT = "/health";
}
