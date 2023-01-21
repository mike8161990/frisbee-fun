using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeBase.Entities
{
    public class TelemetryPointEntity
    {
        public int TelemetryPointId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public double AccelerationX { get; set; }

        public double AccelerationY { get; set; }

        public double AccelerationZ { get; set; }

        public class EntityTypeConfiguration : IEntityTypeConfiguration<TelemetryPointEntity>
        {
            public void Configure(EntityTypeBuilder<TelemetryPointEntity> builder)
            {
                builder.ToTable("telemetry_point");
                builder.HasKey(p => p.TelemetryPointId);
                builder.Property(p => p.Timestamp)
                    .HasConversion(new DateTimeOffsetToBinaryConverter());
            }
        }
    }
}
