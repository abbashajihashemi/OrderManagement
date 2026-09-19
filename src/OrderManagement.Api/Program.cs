using OrderManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddVersioningApis();

var app = builder.Build();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();