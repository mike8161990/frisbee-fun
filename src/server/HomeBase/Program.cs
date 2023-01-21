using HomeBase.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HomeBase;

public class Program
{
    private static void Main(string[] args)
    {
        // Create a persistent connection to prevent database from dropping after context disposal
        var connectionString = "DataSource=:memory:;mode=memory;cache=shared";
        var keepAliveConnection = new SqliteConnection(connectionString);
        keepAliveConnection.Open();

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<FrisbeeContext>(
            options => options.UseSqlite(connectionString));
        builder.Services.AddScoped<ITelemetryService, TelemetryService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Perform one-time schema initialization
        using (var scope = app.Services.CreateScope())
        {
            using var ctx = scope.ServiceProvider.GetRequiredService<FrisbeeContext>();
            ctx.Database.EnsureCreated();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}
