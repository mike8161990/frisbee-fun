using HomeBase.Dtos;

namespace HomeBase.Services;

public interface ITelemetryService
{
    Task CreateFakeCatchEventAsync();

    Task<List<CatchEvent>> GetCatchEventsAsync();
}
