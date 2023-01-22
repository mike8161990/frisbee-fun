namespace HomeBase.Dtos;

public class CatchEventSubmission
{
    public long Timestamp { get; set; }

    public List<TelemetryPointSubmission> TelemetryPoints { get; set; } = new List<TelemetryPointSubmission>();
}
