using Microsoft.EntityFrameworkCore;
using EnvAwareBackend.Models;


var builder = WebApplication.CreateBuilder(args);

// 1. Detect environment (Default to Development)
var env = builder.Environment.EnvironmentName ?? "Development";
// Console.WriteLine($"Current Environment: {environment}");

// 2. Load config.yaml (requires YamlConfiguration NuGet package)
builder.Configuration.AddYamlFile("config.yaml", optional: false, reloadOnChange: true);


// 3. Read environment-specific configurations
var configSection = builder.Configuration.GetSection($"environments:{env.ToLower()}");

string? connString = configSection.GetSection("db:connectionString").Value;
string? accountsTable = configSection.GetSection("tables:accounts").Value;

if (string.IsNullOrEmpty(connString) || string.IsNullOrEmpty(accountsTable))
{
    throw new InvalidOperationException($"Configuration missing for environment: '{env}'. Please check config.yaml.");
}

// 4. Register DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

// 5. Register strongly-typed config object
builder.Services.AddSingleton(new EnvConfig
{
    AccountsTable = accountsTable
});

// Add Controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

//Use minimal hosting model to define a simple endpoint
app.MapControllers();


app.Run();