var builder = WebApplication.CreateBuilder(args);

// Add services to container

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
