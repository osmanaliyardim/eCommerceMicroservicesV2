using eCommerceMicroservicesV2.Discount.Grpc.Data;
using eCommerceMicroservicesV2.Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString(Messages.DISCOUNT_DB_NAME));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
await app.UseMigrationAsync();

app.MapGrpcService<DiscountService>();

app.Run();
