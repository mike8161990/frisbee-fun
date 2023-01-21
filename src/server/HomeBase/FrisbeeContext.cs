using HomeBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBase;

public class FrisbeeContext : DbContext
{
    public DbSet<CatchEventEntity> CatchEvents => this.Set<CatchEventEntity>();

    public DbSet<TelemetryPointEntity> TelemetryPoints => this.Set<TelemetryPointEntity>();

    public FrisbeeContext(DbContextOptions<FrisbeeContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrisbeeContext).Assembly);
    }
}
