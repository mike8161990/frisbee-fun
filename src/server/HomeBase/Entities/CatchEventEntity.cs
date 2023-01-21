using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeBase.Entities;

public class CatchEventEntity
{
    public int CatchEventId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public List<TelemetryPointEntity> TelemetryPoints { get; set; } = new();

    public class CatchEventEntityConfiguration : IEntityTypeConfiguration<CatchEventEntity>
    {
        public void Configure(EntityTypeBuilder<CatchEventEntity> builder)
        {
            builder.ToTable("catch_event");
            builder.HasKey(p => p.CatchEventId);
            builder.Property(p => p.Timestamp)
                .HasConversion(new DateTimeOffsetToBinaryConverter());
            builder.HasMany(p => p.TelemetryPoints)
                .WithOne();
        }
    }
}
