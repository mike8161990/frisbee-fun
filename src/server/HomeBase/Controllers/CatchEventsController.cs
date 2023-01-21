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
    [Route("")]
    public async Task CreateCatchEvent()
    {
        await this.telemetryService.CreateFakeCatchEventAsync();
    }

    [HttpGet]
    [Route("")]
    public async Task<List<CatchEvent>> GetCatchEvents()
    {
        return await this.telemetryService.GetCatchEventsAsync();
    }
}
