namespace HomeBase.Dtos;

public class CatchEvent
{
    public int CatchEventId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public List<TelemetryPoint> TelemetryPoints { get; set; } = new List<TelemetryPoint>();
}
