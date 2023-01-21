namespace HomeBase.Dtos;

public class CatchEvent
{
    public DateTimeOffset Timestamp { get; set; }

    public List<TelemetryPoint> TelemetryPoints { get; set; } = new List<TelemetryPoint>();
}
