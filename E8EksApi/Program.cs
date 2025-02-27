
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;




var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .LogTo(Console.WriteLine, LogLevel.Information));
builder.Services.AddHealthChecks(); 
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
    try
    {
        if (context.Database.CanConnect())
        {
            Console.WriteLine("Database connection successful.");
            var dbName = context.Database.GetDbConnection().Database;
            Console.WriteLine($"Connected to database: {dbName}");
        }
        else
        {
            Console.WriteLine("Database connection failed.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}



app.UseRouting();  
app.UseAuthorization();


app.MapControllers();
app.MapHealthChecks("/health");  

app.Urls.Add("http://*:8080");

app.Run();