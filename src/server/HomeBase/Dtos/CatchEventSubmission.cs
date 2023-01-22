namespace HomeBase.Dtos;

public class CatchEventSubmission
{
    public long T { get; set; }

    public List<TelemetryPointSubmission> P { get; set; } = new List<TelemetryPointSubmission>();
}
