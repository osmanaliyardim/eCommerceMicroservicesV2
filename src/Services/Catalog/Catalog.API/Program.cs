var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var catalogDbConnStr = builder.Configuration.GetConnectionString(Messages.CATALOG_DB_NAME);

// Add services to container
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(catalogDbConnStr!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(catalogDbConnStr!);

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
