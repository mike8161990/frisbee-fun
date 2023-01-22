using HomeBase.Dtos;
using HomeBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBase.Controllers;

[ApiController]
[Route("api/catch-events")]
public class CatchEventsController : ControllerBase
{
    private readonly ITelemetryService telemetryService;

    public CatchEventsController(ITelemetryService telemetryService)
    {
        this.telemetryService = telemetryService;
    }

    [HttpPost]
    [Route("fake")]
    public async Task CreateFakeCatchEvent()
    {
        await this.telemetryService.CreateFakeCatchEventAsync();
    }

    [HttpPost]
    [Route("")]
    public async Task SaveCatchEventsAsync(List<CatchEventSubmission> catchEvents)
    {
        await this.telemetryService.SaveCatchEventsAsync(catchEvents);
    }

    [HttpGet]
    [Route("")]
    public async Task<List<CatchEvent>> GetCatchEvents()
    {
        return await this.telemetryService.GetCatchEventsAsync();
    }
}
