var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var basketDbConnStr = builder.Configuration.GetConnectionString(Messages.BASKET_DB_NAME);
var cacheConnStr = builder.Configuration.GetConnectionString(Messages.REDIS_CACHE_NAME);
var discountGrpcServerUrl = builder.Configuration[Messages.DISCOUNT_GRPC_NAME];

// Add services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(basketDbConnStr!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<BasketInitialData>();
}

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(basketDbConnStr!)
    .AddRedis(cacheConnStr!);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheConnStr;
});

// GRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(discountGrpcServerUrl!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler
            .DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

builder.Services.AddMessageBroker(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks(Messages.HEALTH_CHECK_ENDPOINT,
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
