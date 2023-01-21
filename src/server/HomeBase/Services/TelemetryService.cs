using System.Linq.Expressions;
using HomeBase.Dtos;
using HomeBase.Entities;
using HomeBase.Fakes;
using Microsoft.EntityFrameworkCore;

namespace HomeBase.Services;

public class TelemetryService : ITelemetryService
{
    private readonly FrisbeeContext context;

    public TelemetryService(FrisbeeContext context)
    {
        this.context = context;
    }

    public async Task<List<CatchEvent>> GetCatchEventsAsync()
    {
        return await this.context.CatchEvents
            .OrderByDescending(e => e.Timestamp)
            .Select(MapCatchEvents)
            .ToListAsync();
    }

    public async Task CreateFakeCatchEventAsync()
    {
        var catchEvent = new FakeCatchEvent().Generate();

        this.context.CatchEvents.Add(
            new CatchEventEntity
            {
                Timestamp = catchEvent.Timestamp,
                TelemetryPoints = catchEvent.TelemetryPoints
                .Select(p => new TelemetryPointEntity
                {
                    Timestamp = p.Timestamp,
                    AccelerationX = p.AccelerationX,
                    AccelerationY = p.AccelerationY,
                    AccelerationZ = p.AccelerationZ,
                })
                .ToList(),
            });
        await this.context.SaveChangesAsync();
    }

    private static Expression<Func<CatchEventEntity, CatchEvent>> MapCatchEvents = catchEvent => new CatchEvent
    {
        Timestamp = catchEvent.Timestamp,
        TelemetryPoints = catchEvent.TelemetryPoints
            .Select(telemetryPoint => new TelemetryPoint
            {
                Timestamp = telemetryPoint.Timestamp,
                AccelerationX = telemetryPoint.AccelerationX,
                AccelerationY = telemetryPoint.AccelerationY,
                AccelerationZ = telemetryPoint.AccelerationZ,
            })
            .ToList(),
    };
}